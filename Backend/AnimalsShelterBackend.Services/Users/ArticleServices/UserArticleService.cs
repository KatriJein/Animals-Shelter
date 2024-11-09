using AnimalsShelterBackend.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.ArticleServices
{
	public class UserArticleService : IUserArticleService
	{
		public async Task<List<Article>> GetUserArticlesAsync(IUserService _userService, Guid userId, CancellationToken cancellationToken)
		{
			var user = await _userService.GetByGuidAsync(userId, cancellationToken);
			if (user == null) return [];
			await _userService.LoadUserArticlesAsync(user, cancellationToken);
			return user.Articles;
		}
	}
}
