using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Users
{
	public class UserRepository : BaseRepository<User>
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
	}
}
