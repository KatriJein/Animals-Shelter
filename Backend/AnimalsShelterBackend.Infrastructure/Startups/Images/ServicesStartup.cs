using AnimalsShelterBackend.Services.Images;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalsShelterBackend.Startups.Images
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddImagesServices(this IServiceCollection services)
		{
			services.AddScoped<IFileService, FileService>();
			return services;
		}
	}
}
