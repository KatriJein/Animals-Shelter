using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AnimalsShelterBackend
{
	public static class ControllersStartup
	{
		public static IServiceCollection AddNewControllers(this IServiceCollection services)
		{
			services.AddControllers().AddNewtonsoftJson(setup =>
			{
				setup.SerializerSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));
			}).AddJsonOptions(config => config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			services.AddSwaggerGenNewtonsoftSupport();
			return services;
		}

		public static IServiceCollection AddNewSwaggerGen(this IServiceCollection services)
		{
			services.AddSwaggerGen(swagger =>
			{
				var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				swagger.IncludeXmlComments(xmlPath);
			});
			return services;
		}
	}
}
