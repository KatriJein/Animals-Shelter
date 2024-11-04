using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Articles.Repositories;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Repositories;
using Core.Base.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Users
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddUsersService(this IServiceCollection services)
		{
			services.AddScoped<IService<User>, UserService>();
			return services;
		}
	}
}
