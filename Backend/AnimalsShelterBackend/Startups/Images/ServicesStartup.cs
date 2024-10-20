using AnimalsShelterBackend.Services.Images;

namespace AnimalsShelterBackend.Startups.Images
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddImagesServices(this IServiceCollection services)
		{
			services.AddScoped<IImageService, ImageService>();
			return services;
		}
	}
}
