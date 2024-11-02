using AnimalsShelterBackend.Core.Queries;
using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Images;
using AnimalsShelterBackend.Services.Users;
using Core.Base;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Requests;
using Core.Requests.Articles;
using Core.Responses.Articles;
using Core.Responses.General;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Articles
{
	public class ArticleService : BaseService<Article>, IArticleService
	{
		private readonly IRepository<Article> _articleRepository;
		private readonly IFileService _fileService;
		private readonly IService<User> _userService;
		private readonly ILogger _logger;
		private readonly string _localStorageHost;

		public ArticleService(IRepository<Article> articleRepository, IFileService fileService, IConfiguration config,
			IService<User> userService, ILogger logger) : base(articleRepository)
		{
			_articleRepository = articleRepository;
			_fileService = fileService;
			_userService = userService;
			_logger = logger;
			_localStorageHost = config[Const.MinioLink];
		}

		public override async Task<List<Article>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _articleRepository.GetAll().OrderByDescending(a => a.LastUpdatedAt).ToListAsync(cancellationToken);
		}

		public async Task<CreateArticleResponse> AddAsync(Article article, IFormFile preview, List<IFormFile> files)
		{
			var user = await _userService.GetByGuidAsync(article.UserId, CancellationToken.None);
			if (user == null) return new CreateArticleResponse() { IsSuccess = false, Message = "Статья создана несуществующим пользователем!" };
			await ProcessImagesAndMarkdown(article, preview, files);
			user.Articles.Add(article);
			var guid = await base.AddAsync(article);
			return new CreateArticleResponse() { IsSuccess = true, Id = guid.Id };
		}

		public async override Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var updateArticleRequest = (UpdateArticleRequest)request;
			var article = await GetByGuidAsync(id, CancellationToken.None);
			if (article == null) return new UpdateResponse() { IsSuccess = false, Message = "Попытка обновить несуществующую статью" };
			if (article.UserId != updateArticleRequest.UserId) return new UpdateResponse() { IsSuccess = false, Message = "Предотвращение изменения авторской статьи извне" };
			await DeleteExistingResources(article.BodyMarkDown, article.MainImageSrc);
			article.Title = updateArticleRequest.Title;
			article.Description = updateArticleRequest.Description;
			article.LastUpdatedAt = DateTime.UtcNow;
			article.BodyMarkDown = updateArticleRequest.BodyMarkDown;
			await ProcessImagesAndMarkdown(article, updateArticleRequest.Preview, updateArticleRequest.Files);
			await SaveChangesAsync();
			return new UpdateResponse() { IsSuccess = true };
		}

		public override async Task DeleteAsync(Guid id)
		{
			var article = await GetByGuidAsync(id, CancellationToken.None);
			if (article == null) return;
			await DeleteExistingResources(article.BodyMarkDown, article.MainImageSrc);
			await base.DeleteAsync(id);
		}

		public ArticlesFilesResponse GetFiles(ArticlesFilesQuery filesQuery)
		{
			var files = _fileService.GetFilesFromBucket(Const.NewsArticlesBucketName);
			var filteredFiles = files.Select(fr =>
			{
				fr.Link = $"{_localStorageHost}/{Const.NewsArticlesBucketName}/{fr.Link}";
				return fr;
			}).Where(f => Const.ArticlesAllowableContentTypes.Contains(f.Type));
			filteredFiles = filesQuery == null || !filesQuery.ShowLatest ? filteredFiles :
				filteredFiles.OrderByDescending(f => f.UploadTime);
			return new ArticlesFilesResponse() { IsSuccess = true, Files = filteredFiles.ToList() };
		}

		private async Task ProcessImagesAndMarkdown(Article article, IFormFile preview, List<IFormFile> files)
		{
			var filesToUpload = new List<IFormFile>() { preview };
			files.ForEach(f =>
			{
				if (article.BodyMarkDown.Contains(f.FileName)) filesToUpload.Add(f);
			});
			var imagesSources = FilesUtils.GenerateFileSources(filesToUpload, _localStorageHost, Const.NewsArticlesBucketName);
			article.MainImageSrc = imagesSources[0];
			for (int i = 1; i < filesToUpload.Count; i++)
			{
				article.BodyMarkDown = article.BodyMarkDown.Replace(filesToUpload[i].FileName, imagesSources[i]);
			}
			await _fileService.UploadFiles(Const.NewsArticlesBucketName, filesToUpload, imagesSources);
		}

		private async Task DeleteExistingResources(string markdown, string mainImageSrc)
		{
			var resources = new List<string>() { mainImageSrc };
			var matches = Const.GetUrlsFromArticleRegex.Matches(markdown);
			try
			{
				var bodyResources = matches.Select(m => m.Groups[2].Value).ToList();
				resources.AddRange(bodyResources);
				await _fileService.DeleteFiles(Const.NewsArticlesBucketName, resources);
			}
			catch (Exception e) when (e is IndexOutOfRangeException or ArgumentNullException)
			{
				_logger.Error("Ошибка в обработке регулярного выражения ресурсов! Удаление только обложки...");
				await _fileService.DeleteFiles(Const.NewsArticlesBucketName, resources);
			}
		}
	}
}
