using AnimalsShelterBackend.Domain.ArticleViews;
using AnimalsShelterBackend.Domain.ArticleViews.Repositories;
using AnimalsShelterBackend.Services.Articles;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Requests.Views;
using Core.Responses.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Views
{
	public class ViewsService : BaseService<View>, IViewsService
	{
		private readonly IViewsRepository _viewsRepository;
		private readonly IUserService _userService;
		private readonly IArticleService _articleService;

		public ViewsService(IViewsRepository repository, IUserService userService, IArticleService articleService) : base(repository)
		{
			_viewsRepository = repository;
			_userService = userService;
			_articleService = articleService;
		}

		public async Task<CountViewResponse> CountViewAsync(CountViewRequest countViewRequest)
		{
			var user = await _userService.GetByGuidAsync(countViewRequest.UserId, CancellationToken.None);
			if (user == null) return new CountViewResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			var article = await _articleService.GetByGuidAsync(countViewRequest.ArticleId, CancellationToken.None);
			if (article == null) return new CountViewResponse() { IsSuccess = false, Message = "Несуществующая новость/статья" };
			var exisingView = await _viewsRepository.FindByArticleIdAndUserIdAsync(countViewRequest.ArticleId, countViewRequest.UserId, CancellationToken.None);
			if (exisingView != null) return new CountViewResponse() { IsSuccess = true, Message = "Просмотр уже засчитан, дополнительных действий не требуется" };
			var thisView = new View() { ArticleId = countViewRequest.ArticleId, UserId = countViewRequest.UserId };
			article.ViewsCount++;
			await AddAsync(thisView);
			return new CountViewResponse() { IsSuccess = true };
		}
	}
}
