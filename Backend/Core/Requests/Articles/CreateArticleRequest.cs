using Core.Enums.Articles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Articles
{
	public record CreateArticleRequest(string Title, Tag Tag, Category Category, string Description, IFormFile? Preview, Guid UserId,
		List<IFormFile?>? Files);
}
