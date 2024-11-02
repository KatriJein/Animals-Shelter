using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Services.Users;
using AutoMapper;
using Core.Base.Services;
using Core.Requests.Users;
using Core.Responses.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Users
{
	[Route("api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IService<User> _userService;
		private readonly IMapper _mapper;

		public UsersController(IService<User> userService, IMapper mapper)
		{
			_userService = userService;
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
	}
}
