using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Base.Services;
using Core.Responses.Users.FavouriteAnimalServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.FavouriteAnimalServices
{
	public interface IFavouriteAnimalService
	{
		Task<IsFavouriteAnimalResponse> CheckIsFavouriteAnimalAsync(Guid userId, Guid animalId);
		Task<AddToFavouriteAnimalsResponse> AddAnimalToFavouritesAsync(IUserService _userService, Guid userId, Guid animalId);
		Task<RemoveFromFavouriteAnimalsResponse> RemoveAnimalFromFavouritesAsync(IUserService _userService, Guid userId, Guid animalId);
		Task<RemoveFromFavouriteAnimalsResponse> ClearFavouritesAsync(IUserService _userService, Guid userId);
		Task<List<Animal>> GetFavouriteAnimalsAsync(IUserService _userService, Guid userId, CancellationToken cancellationToken);
	}
}
