using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Services.Notifications;
using AutoMapper;
using Core.Requests.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Notifications
{
	[Route("api/notifications")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly INotificationService _notificationService;
		private readonly IMapper _mapper;

		public NotificationsController(INotificationService notificationService, IMapper mapper)
		{
			_notificationService = notificationService;
			_mapper = mapper;
		}

		/// <summary>
		/// Прислать уведомление всем пользователям
		/// </summary>
		/// <param name="createNotificationRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("notify/all")]
		public async Task<IActionResult> NotifyUsersAsync([FromBody] CreateNotificationRequest createNotificationRequest)
		{
			var notification = _mapper.Map<Notification>(createNotificationRequest);
			await _notificationService.NotifyUsersAsync(notification);
			return Ok();
		}

		/// <summary>
		/// Уведомить конкретного пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="createNotificationRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("notify/{userId}")]
		public async Task<IActionResult> NotifyUserAsync([FromRoute] Guid userId, [FromBody] CreateNotificationRequest createNotificationRequest)
		{
			var notification = _mapper.Map<Notification>(createNotificationRequest);
			var result = await _notificationService.NotifyUserAsync(userId, notification);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok();
		}
	}
}
