﻿using AnimalsShelterBackend.Core.Queries;
using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Images;
using AnimalsShelterBackend.Services.Users;
using Core.Base;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Enums.Articles;
using Core.Queries;
using Core.Redis.Services;
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
		private readonly IRedisService _redisService;
		private readonly IUserService _userService;
		private readonly ILogger _logger;
		private readonly string _localStorageHost;

		public ArticleService(IRepository<Article> articleRepository, IFileService fileService, IConfiguration config,
			IUserService userService, IRedisService redisService, ILogger logger) : base(articleRepository)
		{
			_articleRepository = articleRepository;
			_fileService = fileService;
			_userService = userService;
			_redisService = redisService;
			_logger = logger;
			_localStorageHost = config[Const.MinioLink];
		}

		public override async Task<List<Article>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _articleRepository.GetAll().OrderByDescending(a => a.LastUpdatedAt).ToListAsync(cancellationToken);
		}

		public async Task<CreateArticleResponse> AddAsync(Article article, IFormFile? preview, List<IFormFile?>? files)
		{
			var user = await _userService.GetByGuidAsync(article.UserId, CancellationToken.None);
			if (user == null) return new CreateArticleResponse() { IsSuccess = false, Message = "Статья создана несуществующим пользователем!" };
			article.BodyMarkDown = "empty";
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
			if (article.BodyMarkDown != null)
				await DeleteExistingResources(article.BodyMarkDown, updateArticleRequest.Preview == null ? "" : article.MainImageSrc);
			article.Title = updateArticleRequest.Title ?? article.Title;
			article.Category = updateArticleRequest.Category ?? article.Category;
			article.Tag = updateArticleRequest.Tag ?? article.Tag;
			article.Description = updateArticleRequest.Description ?? article.Description;
			article.LastUpdatedAt = DateTime.UtcNow;
			await ProcessImagesAndMarkdown(article, updateArticleRequest.Preview, updateArticleRequest.Files, true);
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

		private async Task ProcessImagesAndMarkdown(Article article, IFormFile? preview, List<IFormFile?>? files, bool isUpdate=false)
		{
			var filesToUpload = new List<IFormFile>() { };
			if (preview != null) filesToUpload.Add(preview);
			if (files != null)
				files.ForEach(f =>
					{
						if (article.BodyMarkDown.Contains(f.FileName)) filesToUpload.Add(f);
					});
			if (filesToUpload.Count == 0)
			{
				article.MainImageSrc = isUpdate ? article.MainImageSrc : "none";
				return;
			}
			var imagesSources = FilesUtils.GenerateFileSources(filesToUpload, _localStorageHost, Const.NewsArticlesBucketName);
			var preChoose = preview == null ? "none" : imagesSources[0];
			article.MainImageSrc = isUpdate && preChoose == "none" ? article.MainImageSrc : preChoose;
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

		public async Task<List<Article>> GetAllAsync(ArticlesQuery articlesQuery, CancellationToken cancellationToken)
		{
			var articles = await GetAllAsync(cancellationToken);
			if (articlesQuery.Category == null && articlesQuery.SearchBy == null)
				return articles.Where(a => a.Category != Category.News).ToList();
			if (articlesQuery.Category != null)
				return articles.Where(a => a.Category == articlesQuery.Category).ToList();
			return articles.Where(a => a.Title.Contains(articlesQuery.SearchBy) && a.Category != Category.News).ToList();
		}

		public async Task<List<Article>> GetMostPopularAsync(PopularArticlesQuery popularArticlesQuery, CancellationToken cancellationToken)
		{
			var redisKey = Const.MostPopularArticlesKey + popularArticlesQuery.Limit.ToString();
			var response = await CommonUtils.GetWithCachingAsync(_redisService, redisKey, TimeSpan.FromMinutes(5), async () =>
			{
				var query = new ArticlesQuery(null, null);
				var articles = await GetAllAsync(query, cancellationToken);
				var result = articles.OrderByDescending(a => a.ViewsCount).Take(popularArticlesQuery.Limit).ToList();
				return result;
			});
			return response;
		}
	}
}
