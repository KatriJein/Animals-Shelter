using AnimalsShelterBackend.Domain.ShelterUser;
using AutoMapper;
using Core.Requests.Users;
using Core.Responses.General;
using Core.Responses.Users;
using Core.Responses.Users.Auth;
using Core.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.AuthServices
{
	public class AuthService : IAuthService
	{
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;

		public AuthService(IUserService userService, IConfiguration configuration)
		{
			_userService = userService;
			_configuration = configuration;
		}


		public async Task<UserAuthenthicationResponse> AuthenthicateAsync(UserLoginRequest userLoginRequest, IMapper mapper,
			CancellationToken cancellationToken, bool isAutoAuthenthicate=false)
		{
			var user = await _userService.FindUserByLoginAsync(userLoginRequest.Login, cancellationToken);
			if (user == null) return new UserAuthenthicationResponse { IsSuccess = false, Message = "Пользователя с указанным логином не существует" };
			if (!UserUtils.ArePasswordsEqual(userLoginRequest.Password, user.PasswordHash) && !isAutoAuthenthicate)
				return new UserAuthenthicationResponse() { IsSuccess = false, Message = "Неверный пароль" };
			var userInfo = mapper.Map<UserResponse>(user);
			var token = TokenUtils.CreateToken(new UserDataToken() { Id = user.Id, IsAdmin = user.IsAdmin }, DateTime.Now.AddHours(1), _configuration);
			return new UserAuthenthicationResponse() { IsSuccess = true, UserInfo = userInfo, AccessToken = token };
		}

		public async Task<BaseResponse> FinishRegistrationAsync(Guid id, UpdateUserRequest updateUserRequest)
		{
			var response = await _userService.UpdateAsync(id, updateUserRequest);
			return response;
		}

		public async Task<UserRegistrationResponse> RegisterAsync(UserRegisterRequest userRegisterRequest, bool createAdmin=false)
		{
			var userModel = new User();
			var existingUser = await _userService.FindUserByLoginAsync(userRegisterRequest.Login, CancellationToken.None);
			if (existingUser != null) return new UserRegistrationResponse() { IsSuccess = false, Message = "Пользователь с таким логином уже существует" };
			if (userRegisterRequest.Login.Contains('@')) userModel.Email = userRegisterRequest.Login.ToLower();
			else
			{
				var converted = UserUtils.TryConvertPhoneInputToEight(userRegisterRequest.Login, out long phone);
				if (!converted) return new UserRegistrationResponse() { IsSuccess = false, Message = "Требуется почта или телефон в качестве логина" };
				userModel.Phone = phone;
			}
			userModel.PasswordHash = UserUtils.HashPassword(userRegisterRequest.Password);
			if (createAdmin) userModel.IsAdmin = true;
			var entityResponse = await _userService.AddAsync(userModel);
			return new UserRegistrationResponse() { IsSuccess = true, UserId = entityResponse.Id };
		}
	}
}
