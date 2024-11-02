using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Articles.Repositories;
using Core.Base.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Articles
{
	public static class DomainStartup
	{
		public static IServiceCollection AddArticlesDomain(this IServiceCollection services)
		{
			services.AddScoped<IRepository<Article>, ArticleRepository>();
			return services;
		}
	}
}
