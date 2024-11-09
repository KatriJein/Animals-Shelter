using Core.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.ShelterUser.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken);
		Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken);
	}
}
