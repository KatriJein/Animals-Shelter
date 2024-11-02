using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Articles
{
	public record UpdateArticleRequest(string Title, string Description, string BodyMarkDown, IFormFile Preview, Guid UserId, List<IFormFile> Files) : IUpdateRequest;
}
