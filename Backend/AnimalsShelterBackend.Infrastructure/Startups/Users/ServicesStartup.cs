using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Articles.Repositories;
using AnimalsShelterBackend.Services.Users;
using AnimalsShelterBackend.Services.Users.ArticleServices;
using AnimalsShelterBackend.Services.Users.AuthServices;
using AnimalsShelterBackend.Services.Users.FavouriteAnimalServices;
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
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IFavouriteAnimalService, FavouriteAnimalService>();
			services.AddScoped<IUserArticleService, UserArticleService>();
			return services;
		}
	}
}
