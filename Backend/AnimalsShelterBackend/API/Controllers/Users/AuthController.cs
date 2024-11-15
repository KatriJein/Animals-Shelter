using AnimalsShelterBackend.Services.Users.AuthServices;
using AutoMapper;
using Core.Requests.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Users
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AuthController(IAuthService authService, IMapper mapper)
		{
			_authService = authService;
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
			var auth = await _authService.AuthenthicateAsync(new UserLoginRequest(userRegisterRequest.Login, userRegisterRequest.Password), _mapper,
				CancellationToken.None);
			if (!auth.IsSuccess) return StatusCode(501, auth.Message);
			return Ok(auth);
		}
	}
}
