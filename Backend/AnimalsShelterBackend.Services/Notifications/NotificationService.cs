using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Services.Users;
using Core.Base.Services;
using Core.Requests;
using Core.Responses.General;
using Core.Responses.Notifications;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Notifications
{
	public class NotificationService : BaseService<Notification>, INotificationService
	{
		private readonly INotificationRepository _notificationRepository;
		private readonly IUserService _userService;
		private readonly ILogger _logger;

		public NotificationService(INotificationRepository notificationRepository, IUserService userService, ILogger logger) : base(notificationRepository)
		{
			_notificationRepository = notificationRepository;
			_userService = userService;
			_logger = logger;
		}

		public async Task<CreateNotificationResponse> NotifyUserAsync(Guid userId, Notification notification)
		{
			var user = await _userService.GetByGuidAsync(userId, CancellationToken.None);
			if (user == null)
			{
				_logger.Error("Несуществующий пользователь: {userId}. Отправка уведомления невозможна", userId);
				return new CreateNotificationResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			}
			notification.SentAt = DateTime.UtcNow;
			user.Notifications.Add(notification);
			await AddAsync(notification);
			return new CreateNotificationResponse() { IsSuccess = true };
		}

		public async Task NotifyUsersAsync(Notification notification)
		{
			notification.SentAt = DateTime.UtcNow;
			var users = await _userService.GetAllAsync(CancellationToken.None);
			users.ForEach(user =>
			{
				if (!user.IsAdmin)
					user.Notifications.Add(notification);
			});
			await AddAsync(notification);
		}
	}
}
