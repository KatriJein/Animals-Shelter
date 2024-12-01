using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.ShelterUser.Repositories;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Users
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly ShelterAppContext _context;

		public UserRepository(ShelterAppContext context) : base(context, context.Users)
		{
			_context = context;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<User> GetAll()
		{
			return _context.Users;
		}

		public async Task LoadNotificationsAsync(User user, CancellationToken cancellationToken)
		{
			await _context.Entry(user).Collection(u => u.Notifications).LoadAsync(cancellationToken);
		}

		public async Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken)
		{
			await _context.Entry(user).Collection(u => u.Articles).LoadAsync(cancellationToken);
		}

		public async Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken)
		{
			await _context.Entry(user).Collection(u => u.FavouriteAnimals).LoadAsync(cancellationToken);
		}
	}
}
