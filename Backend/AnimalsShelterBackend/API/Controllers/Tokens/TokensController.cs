using AnimalsShelterBackend.Services.RefreshTokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Tokens
{
	[Route("api/tokens")]
	[ApiController]
	public class TokensController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public TokensController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		/// <summary>
		/// Осуществить обновление токенов, используя существующий refresh-токен
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/refresh")]
		public async Task<IActionResult> RefreshTokens([FromRoute] Guid userId)
		{
			var response = await _tokenService.RefreshTokensAsync(userId);
			if (!response.IsSuccess) return BadRequest(response.Message);
			return Ok(response.AccessToken);
		}
	}
}
