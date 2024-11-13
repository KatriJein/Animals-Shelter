using AnimalsShelterBackend.Domain.Animals;
using AutoMapper;
using Core.Base;
using Core.Base.Services;
using Core.Responses.Animals;
using Microsoft.AspNetCore.Http;

namespace AnimalsShelterBackend.Services.Animals
{
	public interface IAnimalsService : IService<Animal>
	{
		Task<CreateEntityResponse> AddAsync(Animal animal, List<IFormFile> files);
		Task LoadUsersForAnimalAsync(Animal animal);
		Task<List<AnimalFullResponse>> GetAllWithIsFavouriteMarkAsync(List<Animal> favourites, IMapper mapper, CancellationToken cancellationToken);
	}
}
