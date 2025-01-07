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
					config.WithOrigins(configuration[Const.FrontendLinkMain], configuration[Const.FrontendLinkAdditional])
					.WithExposedHeaders(Const.TokenHeader, Const.CountHeader)
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});
			return services;
		}
	}
}
