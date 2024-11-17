using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Animals.Repositories;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnimalsShelterBackend.Infrastructure.Animals.Repositories
{
	public class AnimalsRepository : BaseRepository<Animal>, IAnimalsRepository
	{
		private readonly ShelterAppContext _shelterAppContext;

		public AnimalsRepository(ShelterAppContext shelterAppContext) : base(shelterAppContext, shelterAppContext.Animals)
		{
			_shelterAppContext = shelterAppContext;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _shelterAppContext.Animals.Where(a => a.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<Animal> GetAll()
		{
			return _shelterAppContext.Animals;
		}

		public async Task LoadUsersForAnimalAsync(Animal animal)
		{
			await _shelterAppContext.Entry(animal).Collection(a => a.FavouritedByUsers).LoadAsync();
		}
	}
}
