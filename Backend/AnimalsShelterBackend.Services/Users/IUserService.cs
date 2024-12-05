using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Services;
using Core.Queries;
using Core.Requests.Users;
using Core.Responses.Feedbacks;
using Core.Responses.Notifications;
using Core.Responses.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users
{
	public interface IUserService : IService<User>
	{
		Task<UserAvatarUpdateResponse> UpdateUserAvatarAsync(Guid userId, IFormFile? avatar);
		Task<UpdatePasswordResponse> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest updatePasswordRequest);
		Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken);
		Task<User?> FindAdminUserAsync();
		Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken = default);
		Task LoadNotificationsAsync(User user, CancellationToken cancellationToken = default);
		Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken = default);
		Task LoadUserFeedbackAsync(User user, CancellationToken cancellationToken = default);
		Task<GetNotificationsResponse> GetNotificationsAsync(Guid userId, NotificationsQuery notificationsQuery, CancellationToken cancellationToken);
		Task<RemoveNotificationResponse> RemoveNotificationAsync(Guid userId, Guid notificationId);
		Task<GetFeedbackResponse> GetFeedbackAsync(Guid userId, CancellationToken cancellationToken);
		Task<ClearNotificationsResponse> ClearNotificationsAsync(Guid userId);
	}
}
