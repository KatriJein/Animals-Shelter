using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.Tokens;
using AnimalsShelterBackend.Domain.Tokens.Repositories;
using AnimalsShelterBackend.Services.Users;
using Core.Responses.Tokens;
using Core.Responses.Users;
using Core.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.RefreshTokens
{
	public class TokenService: ITokenService
	{
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;

		public TokenService(IRefreshTokenRepository refreshTokenRepository, IUserService userService, IConfiguration configuration)
		{
			_refreshTokenRepository = refreshTokenRepository;
			_userService = userService;
			_configuration = configuration;
		}

		public async Task<CreateOrUpdateTokensResponse> CreateOrUpdateTokensAsync(User user)
		{
			await _refreshTokenRepository.RemoveByUserIdAsync(user.Id);
			var requiredUserData = new UserDataToken() { Id = user.Id, IsAdmin = user.IsAdmin };
			var accessToken = TokenUtils.CreateToken(requiredUserData, DateTime.UtcNow.AddSeconds(15), _configuration);
			var refreshTokenExpirationDate = DateTime.UtcNow.AddSeconds(30);
			var refreshToken = TokenUtils.CreateToken(requiredUserData, refreshTokenExpirationDate, _configuration);
			var refreshTokenModel = new RefreshToken() { ExpiresAt = refreshTokenExpirationDate, Token = refreshToken };
			user.RefreshToken = refreshTokenModel;
			await _refreshTokenRepository.AddAsync(refreshTokenModel);
			return new CreateOrUpdateTokensResponse() { IsSuccess = true, AccessToken = accessToken };
		}

		public async Task<RefreshToken?> GetByUserIdAsync(Guid userId)
		{
			return await _refreshTokenRepository.GetByUserIdAsync(userId);
		}

		public async Task<CreateOrUpdateTokensResponse> RefreshTokensAsync(Guid userId)
		{
			var user = await _userService.GetByGuidAsync(userId, CancellationToken.None);
			if (user == null)
				return new CreateOrUpdateTokensResponse() { IsSuccess = false, Message = "Пользователь не существует", AccessToken = "" };
			var token = await _refreshTokenRepository.GetByUserIdAsync(userId);
			if (token == null || token.ExpiresAt < DateTime.UtcNow)
				return new CreateOrUpdateTokensResponse() { IsSuccess = false, Message = "Истек срок действия Refresh-токена или он не существует", AccessToken = "" };
			return await CreateOrUpdateTokensAsync(user);
		}

		public async Task RemoveByUserIdAsync(Guid userId)
		{
			await _refreshTokenRepository.RemoveByUserIdAsync(userId);
		}

		public async Task SaveChangesAsync()
		{
			await _refreshTokenRepository.SaveChangesAsync();
		}
	}
}
