using Core.Constants;
using Core.Redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis
{
	public static class RedisStartup
	{
		public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(setup =>
			{
				setup.Configuration = configuration[Const.RedisLink];
			});
			services.AddScoped<IRedisService, RedisService>();
			return services;
		}
	}
}
