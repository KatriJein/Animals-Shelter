using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Animals;
using Core.Base.Services;
using Core.Responses.Users.FavouriteAnimalServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.FavouriteAnimalServices
{
	public class FavouriteAnimalService : IFavouriteAnimalService
	{
		private readonly IAnimalsService _animalService;

		public FavouriteAnimalService(IAnimalsService animalService)
		{
			_animalService = animalService;
		}

		public async Task<AddToFavouriteAnimalsResponse> AddAnimalToFavouritesAsync(IUserService _userService, Guid userId, Guid animalId)
		{
			var user = await _userService.GetByGuidAsync(userId, CancellationToken.None);
			if (user == null) return new AddToFavouriteAnimalsResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			var animal = await _animalService.GetByGuidAsync(animalId, CancellationToken.None);
			if (animal == null) return new AddToFavouriteAnimalsResponse() { IsSuccess = false, Message = "Несуществующее животное" };
			await _animalService.LoadUsersForAnimalAsync(animal);
			var isNotInFavourites = animal.FavouritedByUsers.All(u => u.Id != userId);
			if (isNotInFavourites)
			{
				animal.FavouritedByUsers.Add(user);
				await _animalService.SaveChangesAsync();
			}
			return new AddToFavouriteAnimalsResponse() { IsSuccess = true, Message = isNotInFavourites ? ""
				: "Животное уже добавлено в избранное, никаких дополнительных действий не совершено" };
		}

		public async Task<IsFavouriteAnimalResponse> CheckIsFavouriteAnimalAsync(Guid userId, Guid animalId)
		{
			var requiredAnimal = await _animalService.GetByGuidAsync(animalId, CancellationToken.None);
			if (requiredAnimal == null) return new IsFavouriteAnimalResponse() { IsSuccess = false, Message = "Несуществующее животное" };
			await _animalService.LoadUsersForAnimalAsync(requiredAnimal);
			var hasInFavourites = requiredAnimal.FavouritedByUsers.Any(u => u.Id == userId);
			return new IsFavouriteAnimalResponse() { IsSuccess = true, IsFavouriteAnimal = hasInFavourites };
		}

		public async Task<RemoveFromFavouriteAnimalsResponse> RemoveAnimalFromFavouritesAsync(IUserService _userService, Guid userId, Guid animalId)
		{
			var user = await _userService.GetByGuidAsync(userId, CancellationToken.None);
			if (user == null) return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			var animal = await _animalService.GetByGuidAsync(animalId, CancellationToken.None);
			if (animal == null) return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = false, Message = "Несуществующее животное" };
			await _animalService.LoadUsersForAnimalAsync(animal);
			var isInFavourites = animal.FavouritedByUsers.Any(u => u.Id == userId);
			if (!isInFavourites) return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = false, Message = "Животное уже не находится в избранном у пользователя" };
			animal.FavouritedByUsers.Remove(user);
			await _animalService.SaveChangesAsync();
			return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = true };
		}

		

		public async Task<List<Animal>> GetFavouriteAnimalsAsync(IUserService _userService, Guid userId, CancellationToken cancellationToken)
		{
			var user = await _userService.GetByGuidAsync(userId, cancellationToken);
			if (user == null) return [];
			await _userService.LoadUserFavouriteAnimalsAsync(user, cancellationToken);
			return user.FavouriteAnimals;
		}

		public async Task<RemoveFromFavouriteAnimalsResponse> ClearFavouritesAsync(IUserService _userService, Guid userId)
		{
			var user = await _userService.GetByGuidAsync(userId, CancellationToken.None);
			if (user == null) return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = false, Message = "Несуществующий пользователь" };
			await _userService.LoadUserFavouriteAnimalsAsync(user, CancellationToken.None);
			user.FavouriteAnimals.Clear();
			await _userService.SaveChangesAsync();
			return new RemoveFromFavouriteAnimalsResponse() { IsSuccess = true };
		}
	}
}
