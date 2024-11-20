using AnimalsShelterBackend.Services.RefreshTokens;
using Core.Constants;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace AnimalsShelterBackend.Middleware
{
	public class AuthMiddleware
	{
		private readonly RequestDelegate _next;

		public AuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var tokenService = context.RequestServices.GetRequiredService<ITokenService>();
			var authorizationHeaderData = context.Request.Headers.Authorization;
			if (authorizationHeaderData.Count != 0 && authorizationHeaderData[0].Contains("bearer"))
			{
				var jwtToken = authorizationHeaderData[0].Split(" ")[1];
				var tokenData = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
			    if (tokenData.ValidTo < DateTime.UtcNow)
				{
					var correspondingUser = tokenData.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
					if (correspondingUser != null)
					{
						var userId = Guid.Parse(correspondingUser);
						var tokenResponse = await tokenService.RefreshTokensAsync(userId);
						if (tokenResponse.IsSuccess) context.Response.Headers.Append(Const.TokenHeader, tokenResponse.AccessToken);
						else context.Response.Headers.Append(Const.TokenHeader, "expired");
					}
				}
			}
			await _next(context);
		}
	}
}
