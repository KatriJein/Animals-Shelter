﻿using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Users;
using AnimalsShelterBackend.Services.Users.ArticleServices;
using AnimalsShelterBackend.Services.Users.FavouriteAnimalServices;
using AutoMapper;
using Core.Base.Services;
using Core.Queries;
using Core.Requests.Users;
using Core.Responses.Animals;
using Core.Responses.Articles;
using Core.Responses.Notifications;
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
		/// Получить пользователя по Guid
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid id, CancellationToken cancellationToken)
		{
			var user = await _userService.GetByGuidAsync(id, cancellationToken);
			if (user == null) return NoContent();
			var mappedUser = _mapper.Map<UserResponse>(user);
			return Ok(mappedUser);
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
		/// Обновить личные данные пользователя (кроме пароля и аватарки)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateUserRequest"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest updateUserRequest)
		{
			var response = await _userService.UpdateAsync(id, updateUserRequest);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok();
		}

		/// <summary>
		/// Обновить картинку профиля пользователя
		/// </summary>
		/// <param name="id"></param>
		/// <param name="avatarRequest"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("{id}/avatar")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UpdateAvatarAsync([FromRoute] Guid id, [FromForm] UpdateUserAvatarRequest avatarRequest)
		{
			var response = await _userService.UpdateUserAvatarAsync(id, avatarRequest.Avatar);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok();
		}

		/// <summary>
		/// Обновить пароль пользователя
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updatePasswordRequest"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("{id}/password")]
		public async Task<IActionResult> UpdatePasswordAsync([FromRoute] Guid id, [FromBody] UpdatePasswordRequest updatePasswordRequest)
		{
			var response = await _userService.UpdatePasswordAsync(id, updatePasswordRequest);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok();
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

		/// <summary>
		/// Очистить список избранных животных
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{userId}/favourites/clear")]
		public async Task<IActionResult> ClearFavouritesAsync([FromRoute] Guid userId)
		{
			var result = await _favouriteAnimalService.ClearFavouritesAsync(_userService, userId);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok(result);
		}

		/**
		/// <summary>
		/// Получить все уведомления пользователя или только непрочитанные
		/// </summary>
		/// <param name="notificationsQuery"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/notifications")]
		public async Task<IActionResult> GetNotificationsAsync([FromQuery] NotificationsQuery notificationsQuery, [FromRoute] Guid userId, CancellationToken cancellationToken)
		{
			var notificationsResponse = await _userService.GetNotificationsAsync(userId, notificationsQuery, cancellationToken);
			if (!notificationsResponse.IsSuccess) return BadRequest(notificationsResponse.Message);
			return Ok(notificationsResponse.Notifications);
		}
		**/

		/**
		/// <summary>
		/// Очистить уведомления пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{userId}/notifications/clear")]
		public async Task<IActionResult> ClearNotificationsAsync([FromRoute] Guid userId)
		{
			var response = await _userService.ClearNotificationsAsync(userId);
			if (!response.IsSuccess) return BadRequest(response.Message);
			return Ok();
		}
		**/

		/**
		/// <summary>
		/// Удалить уведомление пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{userId}/notifications/{notificationId}/remove")]
		public async Task<IActionResult> RemoveNotificationAsync([FromRoute] Guid userId, [FromRoute] Guid notificationId)
		{
			var response = await _userService.RemoveNotificationAsync(userId, notificationId);
			if (!response.IsSuccess) return BadRequest(response.Message);
			return Ok();
		}
		**/

		/**
		/// <summary>
		/// Получить отзыв пользователя
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/feedback")]
		public async Task<IActionResult> GetFeedbackAsync([FromRoute] Guid userId, CancellationToken cancellationToken)
		{
			var response = await _userService.GetFeedbackAsync(userId, cancellationToken);
			if (!response.IsSuccess) return BadRequest(response.Message);
			if (response.FeedbackResponse == null) return NoContent();
			return Ok(response.FeedbackResponse);
		}
		**/
	}
}
