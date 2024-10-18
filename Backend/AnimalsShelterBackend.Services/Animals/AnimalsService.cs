using AnimalsShelterBackend.Domain.Animals;
using Core.Base.Repositories;
using Core.Base.Services;

namespace AnimalsShelterBackend.Services.Animals
{
	public class AnimalsService : BaseService<Animal>
	{
		private readonly IRepository<Animal> _animalsRepository;

		public AnimalsService(IRepository<Animal> animalsRepository) : base(animalsRepository)
		{
			_animalsRepository = animalsRepository;
		}
	}
}
