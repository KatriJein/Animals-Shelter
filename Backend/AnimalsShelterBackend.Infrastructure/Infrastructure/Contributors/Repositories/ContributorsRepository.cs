using AnimalsShelterBackend.Domain.Contributors;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Contributors.Repositories
{
	public class ContributorsRepository : BaseRepository<Contributor>, IContributorsRepository
	{
		private readonly ShelterAppContext _shelterAppContext;

		public ContributorsRepository(ShelterAppContext shelterAppContext): base(shelterAppContext, shelterAppContext.Contributors)
		{
			_shelterAppContext = shelterAppContext;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _shelterAppContext.Contributors.Where(c => c.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<Contributor> GetAll()
		{
			return _shelterAppContext.Contributors;
		}
	}
}
