using AnimalsShelterBackend.Core.Queries;
using AnimalsShelterBackend.Domain.Articles;
using Core.Base;
using Core.Base.Services;
using Core.Responses.Articles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Articles
{
	public interface IArticleService : IService<Article>
	{
		ArticlesFilesResponse GetFiles(ArticlesFilesQuery filesQuery);
		Task<CreateArticleResponse> AddAsync(Article article, IFormFile preview, List<IFormFile> files);
	}
}
