using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Notifications;
using AnimalsShelterBackend.Services.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Startups.Notifications
{
	public static class Startup
	{
		public static IServiceCollection AddNotifications(this IServiceCollection services)
		{
			services.AddScoped<INotificationRepository, NotificationRepository>();
			services.AddScoped<INotificationService, NotificationService>();
			return services;
		}
	}
}
