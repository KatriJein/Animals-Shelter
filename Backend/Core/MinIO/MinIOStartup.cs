using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MinIO
{
	public static class MinIOStartup
	{
		public static IServiceCollection AddMinIOStorage(this IServiceCollection services, IConfiguration config)
		{
			var minioOptions = new MiniOptions();
			config.GetSection("Minio").Bind(minioOptions);
			services.AddMinio(config =>
			{
				config.WithEndpoint(minioOptions.Endpoint)
				.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
				.WithSSL(false)
				.Build();
			});
			return services;
		}
	}
}
