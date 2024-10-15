using Core.Options;

namespace AnimalsShelterBackend.Infrastructure.Configurations
{
	public static class OptionsConfigurations
	{
		public static IServiceCollection ConfigureDbConnection(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<ShelterAppDbContextOptions>(config.GetSection("PostgresSQL"));
			return services;
		}
	}
}
