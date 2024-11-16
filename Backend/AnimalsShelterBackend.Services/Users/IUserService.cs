using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Services;
using Core.Requests.Users;
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
		Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken);
		Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken = default);
		Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken = default);
	}
}
