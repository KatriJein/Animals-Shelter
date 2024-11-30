using AnimalsShelterBackend.Domain.Tokens.Repositories;
using AnimalsShelterBackend.Infrastructure.Infrastructure.RefreshTokens;
using AnimalsShelterBackend.Services.RefreshTokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.RefreshTokens
{
	public static class Startup
	{
		public static IServiceCollection AddTokens(this IServiceCollection services)
		{
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<ITokenService, TokenService>();
			return services;
		}
	}
}
