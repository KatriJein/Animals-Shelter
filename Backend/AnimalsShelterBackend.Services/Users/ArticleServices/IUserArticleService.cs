using AnimalsShelterBackend.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.ArticleServices
{
	public interface IUserArticleService
	{
		Task<List<Article>> GetUserArticlesAsync(IUserService _userService, Guid userId, CancellationToken cancellationToken);
	}
}
