using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users
{
	public interface IUserService : IService<User>
	{
		Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken = default);
		Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken = default);
	}
}
