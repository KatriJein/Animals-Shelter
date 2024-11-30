using AnimalsShelterBackend.Domain.Contributors;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Contributors.Repositories;
using AnimalsShelterBackend.Services.Contributors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Contributors
{
	public static class Startup
	{
		public static IServiceCollection AddContributors(this IServiceCollection services)
		{
			services.AddScoped<IContributorsRepository, ContributorsRepository>();
			services.AddScoped<IContributorsService, ContributorService>();
			return services;
		}
	}
}
