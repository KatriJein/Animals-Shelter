using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Services.Notifications;
using AutoMapper;
using Core.Requests.Notifications;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Masstransit.Consumers
{
	public class NotificationsConsumer : IConsumer<CreateNotificationRequest>
	{
		private readonly INotificationService _notificationService;
		private readonly IMapper _mapper;

		public NotificationsConsumer(INotificationService notificationService, IMapper mapper)
		{
			_notificationService = notificationService;
			_mapper = mapper;
		}

		public async Task Consume(ConsumeContext<CreateNotificationRequest> context)
		{
			var notification = _mapper.Map<Notification>(context.Message);
			await _notificationService.NotifyUsersAsync(notification);
		}
	}
}
