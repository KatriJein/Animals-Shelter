using Core.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalsShelterBackend.Startups
{
	public static class CORSStartup
	{
		public static IServiceCollection AppendCors(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(setup =>
			{
				setup.AddPolicy(Const.FrontendCORS, config =>
				{
					config.WithOrigins(configuration[Const.FrontendLink])
					.WithExposedHeaders(Const.TokenHeader)
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});
			return services;
		}
	}
}
