using AnimalsShelterBackend.Services.Articles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Articles
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddArticlesServices(this IServiceCollection services)
		{
			services.AddScoped<IArticleService, ArticleService>();
			return services;
		}
	}
}
