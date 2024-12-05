using AnimalsShelterBackend.Services.BusServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Masstransit
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddMasstransitAbstractionServices(this IServiceCollection services)
		{
			services.AddScoped<INotificationsBusService, NotificationsBusService>();
			return services;
		}
	}
}
