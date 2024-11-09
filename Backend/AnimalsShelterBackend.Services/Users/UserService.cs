using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.ShelterUser.Repositories;
using Core.Base.Repositories;
using Core.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users
{
	public class UserService : BaseService<User>, IUserService
	{
		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository) : base(repository)
		{
			_repository = repository;
		}

		public async Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken = default)
		{
			await _repository.LoadUserArticlesAsync(user, cancellationToken);
		}

		public async Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken = default)
		{
			await _repository.LoadUserFavouriteAnimalsAsync(user, cancellationToken);
		}
	}
}
