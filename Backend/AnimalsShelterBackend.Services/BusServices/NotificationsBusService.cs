using AnimalsShelterBackend.Domain.Animals;
using Core.Constants;
using Core.Requests.Notifications;
using MassTransit;
using System;
using Core.Enums.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.BusServices
{
	public class NotificationsBusService : INotificationsBusService
	{
		private readonly IBus _bus;

		public NotificationsBusService(IBus bus)
		{
			_bus = bus;
		}

		public async Task SendNewAnimalInfoAsync(Animal animalData, Guid animalId)
		{
			var request = new CreateNotificationRequest("Поступление животного!",
				$"В приют поступило новое животное: {animalData.Name}, возраст: {animalData.Age}", Clickable: true,
				string.Format(Const.NotificationLinkPatterns[NotificationType.NewAnimal], animalId));
			await _bus.Publish(request);
		}
	}
}
