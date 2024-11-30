using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Services;
using Core.Responses.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Notifications
{
	public interface INotificationService : IService<Notification>
	{
		Task NotifyUsersAsync(Notification notification);
		Task<CreateNotificationResponse> NotifyUserAsync(Guid userId, Notification notification);
	}
}
