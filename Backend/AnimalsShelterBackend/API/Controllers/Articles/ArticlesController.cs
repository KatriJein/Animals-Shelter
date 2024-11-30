
using AnimalsShelterBackend.Core.Queries;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Services.Articles;
using AutoMapper;
using Core.Constants;
using Core.Queries;
using Core.Requests.Articles;
using Core.Responses.Articles;
using Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Articles
{
	[Route("api/articles")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private readonly IArticleService _articleService;
		private readonly IMapper _mapper;

		public ArticlesController(IArticleService articleService, IMapper mapper)
		{
			_articleService = articleService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить все файлы из статей и новостей. Возможно получение по убыванию даты
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("files")]
		public async Task<IActionResult> GetAllFiles([FromQuery] ArticlesFilesQuery filesQuery)
		{
			var res = _articleService.GetFiles(filesQuery);
			return Ok(res);
		}

		/// <summary>
		/// Получить все новости или статьи
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="articlesQuery"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync([FromQuery] ArticlesQuery articlesQuery, CancellationToken cancellationToken)
		{
			var articles = await _articleService.GetAllAsync(articlesQuery, cancellationToken);
			var mappedArticles = _mapper.Map<List<ArticleShortResponse>>(articles);
			CommonUtils.AddHeaderToResponse(HttpContext, Const.CountHeader, articles.Count);
			return Ok(mappedArticles);
		}

		/// <summary>
		/// Получить топ-N самых популярных статей
		/// </summary>
		/// <param name="popularArticlesQuery"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("popular")]
		public async Task<IActionResult> GetMostPopularAsync([FromQuery] PopularArticlesQuery popularArticlesQuery, CancellationToken cancellationToken)
		{
			var articles = await _articleService.GetMostPopularAsync(popularArticlesQuery, cancellationToken);
			var mappedArticles = _mapper.Map<List<ArticleShortResponse>>(articles);
			return Ok(mappedArticles);
		}

		/// <summary>
		/// Получить отдельную новость или статью
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
		{
			var article = await _articleService.GetByGuidAsync(id, cancellationToken);
			if (article == null) return NoContent();
			var mappedArticle = _mapper.Map<ArticleFullResponse>(article);
			return Ok(mappedArticle);
		}

		/// <summary>
		/// Публикация новости или статьи
		/// </summary>
		/// <param name="createArticleRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> CreateAsync([FromForm] CreateArticleRequest createArticleRequest)
		{
			var article = _mapper.Map<Article>(createArticleRequest);
			var res = await _articleService.AddAsync(article, createArticleRequest.Preview, createArticleRequest.Files);
			if (!res.IsSuccess) return BadRequest(res.Message);
			return Ok(res);
		}

		/// <summary>
		/// Обновить новость или статью
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateArticleRequest updateArticleRequest)
		{
			var res = await _articleService.UpdateAsync(id, updateArticleRequest);
			if (!res.IsSuccess) return BadRequest(res.Message);
			return Ok(res);
		}


		/// <summary>
		/// Удалить новость или статью
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
		{
			await _articleService.DeleteAsync(id);
			return Ok();
		}
	}
}
