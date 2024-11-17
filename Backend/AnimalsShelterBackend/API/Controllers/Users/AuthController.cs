using AnimalsShelterBackend.Services.Users;
using AnimalsShelterBackend.Services.Users.AuthServices;
using AutoMapper;
using Core.Requests.Users;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Users
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
		{
			_authService = authService;
			_userService = userService;
			_mapper = mapper;
		}

		/// <summary>
		/// Выполнить аутентификацию пользователя
		/// </summary>
		/// <param name="userLoginRequest"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> AuthenthicateAsync([FromBody] UserLoginRequest userLoginRequest, CancellationToken cancellationToken)
		{
			var response = await _authService.AuthenthicateAsync(userLoginRequest, _mapper, cancellationToken);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok(response);
		}

		/// <summary>
		/// Выполнить регистрацию пользователя и автоматический вход в аккаунт
		/// </summary>
		/// <param name="userRegisterRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest userRegisterRequest)
		{
			var response = await _authService.RegisterAsync(userRegisterRequest);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok(response);
		}

		/// <summary>
		/// Завершить регистрацию пользователя, указав дополнительные данные
		/// </summary>
		/// <param name="updateUserRequest"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{id}/finish")]
		public async Task<IActionResult> FinishRegistrationAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest updateUserRequest)
		{
			var response = await _authService.FinishRegistrationAsync(id, updateUserRequest);
			if (!response.IsSuccess) return BadRequest(response);
			var user = await _userService.GetByGuidAsync(id, CancellationToken.None);
			var loginRequest = new UserLoginRequest(user.Email ?? UserUtils.ConvertPhoneToPlusSeven(user.Phone), "");
			var authResponse = await _authService.AuthenthicateAsync(loginRequest, _mapper, CancellationToken.None, isAutoAuthenthicate:true);
			if (!authResponse.IsSuccess) return StatusCode(501);
			return Ok(authResponse);
		}
	}
}
