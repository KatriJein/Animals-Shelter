
using AnimalsShelterBackend.Infrastructure.Masstransit.Consumers;
using Core.Constants;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Masstransit
{
	public static class MassTransitStartup
	{
		public static IServiceCollection UseMasstransitRabbitMQ(this IServiceCollection services, IConfiguration config)
		{
			var rabbitMqSettingsSection = config.GetSection(Const.RabbitMQ);
			services.AddMassTransit(mtConfig =>
			{
				mtConfig.SetKebabCaseEndpointNameFormatter();
				mtConfig.AddConsumer<NotificationsConsumer>();
				mtConfig.UsingRabbitMq((busContext, rmqConfig) =>
				{
					rmqConfig.Host(rabbitMqSettingsSection["Host"], hostConfig =>
					{
						hostConfig.Username(rabbitMqSettingsSection["Username"]);
						hostConfig.Password(rabbitMqSettingsSection["Password"]);
					});
					rmqConfig.ReceiveEndpoint(Const.NotificationsQueue, config =>
					{
						config.ConfigureConsumer<NotificationsConsumer>(busContext);
					});
					rmqConfig.ConfigureEndpoints(busContext);
				});
			});
			return services;
		}
	}
}
