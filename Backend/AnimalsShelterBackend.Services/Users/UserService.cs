using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Repositories;
using Core.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users
{
	public class UserService : BaseService<User>
	{
		private readonly IRepository<User> _repository;

		public UserService(IRepository<User> repository) : base(repository)
		{
			_repository = repository;
		}
	}
}
