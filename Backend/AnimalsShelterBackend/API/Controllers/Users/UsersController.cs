using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Users;
using AnimalsShelterBackend.Services.Users.ArticleServices;
using AnimalsShelterBackend.Services.Users.FavouriteAnimalServices;
using AutoMapper;
using Core.Base.Services;
using Core.Requests.Users;
using Core.Responses.Animals;
using Core.Responses.Articles;
using Core.Responses.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Users
{
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IFavouriteAnimalService _favouriteAnimalService;
		private readonly IUserArticleService _userArticleService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService, IMapper mapper, IFavouriteAnimalService favouriteAnimalService,
			IUserArticleService userArticleService)
		{
			_userService = userService;
			_favouriteAnimalService = favouriteAnimalService;
			_userArticleService = userArticleService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			var users = await _userService.GetAllAsync(cancellationToken);
			var mappedUsers = _mapper.Map<List<UserResponse>>(users);
			return Ok(mappedUsers);
		}

		/// <summary>
		/// Получить список избранных животных пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/favourites")]
		public async Task<IActionResult> GetFavouriteAnimalsAsync([FromRoute] Guid userId, CancellationToken cancellationToken)
		{
			var animals = await _favouriteAnimalService.GetFavouriteAnimalsAsync(_userService, userId, cancellationToken);
			if (animals.Count == 0) return NoContent();
			var mappedAnimals = _mapper.Map<List<AnimalFullResponse>>(animals);
			mappedAnimals.ForEach(ma => ma.IsFavourite = true);
			return Ok(mappedAnimals);
		}

		/// <summary>
		/// Получить список статей и новостей, написанных пользователем
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/articles")]
		public async Task<IActionResult> GetArticlesAsync([FromRoute] Guid userId, CancellationToken cancellationToken)
		{
			var articles = await _userArticleService.GetUserArticlesAsync(_userService, userId, cancellationToken);
			if (articles.Count == 0) return NoContent();
			var mappedArticles = _mapper.Map<List<ArticleShortResponse>>(articles);
			return Ok(mappedArticles);
		}

		/// <summary>
		/// Создание нового пользователя
		/// </summary>
		/// <param name="createUserRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest createUserRequest)
		{
			var userModel = _mapper.Map<User>(createUserRequest);
			var res = await _userService.AddAsync(userModel);
			return Ok(res);
		}

		/// <summary>
		/// Удаление пользователя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
		{
			await _userService.DeleteAsync(id);
			return Ok();
		}

		/// <summary>
		/// Проверить, является ли животное избранным у пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="animalId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/favourites/{animalId}/exists")]
		public async Task<IActionResult> IsAnimalInFavouritesAsync([FromRoute] Guid userId, [FromRoute] Guid animalId)
		{
			var checkResult = await _favouriteAnimalService.CheckIsFavouriteAnimalAsync(userId, animalId);
			if (!checkResult.IsSuccess) return BadRequest(checkResult.Message);
			return Ok(checkResult.IsFavouriteAnimal);
		}

		/// <summary>
		/// Добавить животное в избранное
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="animalId"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("{userId}/favourite/{animalId}")]
		public async Task<IActionResult> AddAnimalToFavouritesAsync([FromRoute] Guid userId, [FromRoute] Guid animalId)
		{
			var result = await _favouriteAnimalService.AddAnimalToFavouritesAsync(_userService, userId, animalId);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok(result);
		}

		/// <summary>
		/// Удалить животное из избранного
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="animalId"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("{userId}/unfavourite/{animalId}")]
		public async Task<IActionResult> RemoveAnimalFromFavouritesAsync([FromRoute] Guid userId, [FromRoute] Guid animalId)
		{
			var result = await _favouriteAnimalService.RemoveAnimalFromFavouritesAsync(_userService, userId, animalId);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok(result);
		}
	}
}
