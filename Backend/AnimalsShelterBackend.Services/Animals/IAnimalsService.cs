using AnimalsShelterBackend.Domain.Animals;
using Core.Base;
using Core.Base.Services;
using Microsoft.AspNetCore.Http;

namespace AnimalsShelterBackend.Services.Animals
{
	public interface IAnimalsService : IService<Animal>
	{
		Task<CreateEntityResponse> AddAsync(Animal animal, List<IFormFile> files);
	}
}
