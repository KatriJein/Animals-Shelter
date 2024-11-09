using Core.Base.Repositories;

namespace AnimalsShelterBackend.Domain.Animals.Repositories
{
	public interface IAnimalsRepository : IRepository<Animal>
	{
		Task LoadUsersForAnimalAsync(Animal animal);
	}
}
