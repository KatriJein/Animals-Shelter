using Core.MinIO;
using Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalsShelterBackend.Infrastructure.Configurations
{
	public static class OptionsConfigurations
	{
		public static IServiceCollection ConfigureDbConnection(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<ShelterAppDbContextOptions>(config.GetSection("PostgresSQL"));
			return services;
		}

		public static IServiceCollection ConfigureMinio(this IServiceCollection services, IConfiguration config)
		{
			services.Configure<MiniOptions>(config.GetSection("Minio"));
			return services;
		}
	}
}
