using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.ShelterUser.Repositories;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Articles.Repositories;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Users;
using Core.Base.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Users
{
	public static class DomainStartup
	{
		public static IServiceCollection AddUsersDomain(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			return services;
		}
	}
}
