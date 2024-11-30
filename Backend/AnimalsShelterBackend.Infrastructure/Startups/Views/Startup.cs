using AnimalsShelterBackend.Domain.ArticleViews.Repositories;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Views;
using AnimalsShelterBackend.Services.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Views
{
	public static class Startup
	{
		public static IServiceCollection AddArticleViewsSupport(this IServiceCollection services)
		{
			services.AddScoped<IViewsRepository, ViewsRepository>();
			services.AddScoped<IViewsService, ViewsService>();
			return services;
		}
	}
}
