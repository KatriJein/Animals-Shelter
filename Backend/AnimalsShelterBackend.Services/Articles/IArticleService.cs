using AnimalsShelterBackend.Core.Queries;
using AnimalsShelterBackend.Domain.Articles;
using Core.Base;
using Core.Base.Services;
using Core.Queries;
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
		Task<List<Article>> GetAllAsync(ArticlesQuery articlesQuery, CancellationToken cancellationToken);
		ArticlesFilesResponse GetFiles(ArticlesFilesQuery filesQuery);
		Task<CreateArticleResponse> AddAsync(Article article, IFormFile? preview, List<IFormFile?>? files);
	}
}
