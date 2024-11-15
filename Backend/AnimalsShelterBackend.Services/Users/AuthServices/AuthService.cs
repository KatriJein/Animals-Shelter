using AnimalsShelterBackend.Domain.ShelterUser;
using AutoMapper;
using Core.Requests.Users;
using Core.Responses.Users;
using Core.Responses.Users.Auth;
using Core.Utils;
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

		public AuthService(IUserService userService)
		{
			_userService = userService;
		}


		public async Task<UserAuthenthicationResponse> AuthenthicateAsync(UserLoginRequest userLoginRequest, IMapper mapper,
			CancellationToken cancellationToken)
		{
			var user = await _userService.FindUserByLoginAsync(userLoginRequest.Login, cancellationToken);
			if (user == null) return new UserAuthenthicationResponse { IsSuccess = false, Message = "Пользователя с указанным логином не существует" };
			if (!UserUtils.ArePasswordsEqual(userLoginRequest.Password, user.PasswordHash))
				return new UserAuthenthicationResponse() { IsSuccess = false, Message = "Неверный пароль" };
			var userInfo = mapper.Map<UserResponse>(user);
			return new UserAuthenthicationResponse() { IsSuccess = true, UserInfo = userInfo };
		}

		public async Task<UserRegistrationResponse> RegisterAsync(UserRegisterRequest userRegisterRequest)
		{
			var userModel = new User();
			var existingUser = await _userService.FindUserByLoginAsync(userRegisterRequest.Login, CancellationToken.None);
			if (existingUser != null) return new UserRegistrationResponse() { IsSuccess = false, Message = "Пользователь с таким логином уже существует" };
			if (userRegisterRequest.Login.Contains('@')) userModel.Email = userRegisterRequest.Login.ToLower();
			else userModel.Phone = UserUtils.ConvertPhoneInputToEight(userRegisterRequest.Login);
			userModel.PasswordHash = UserUtils.HashPassword(userRegisterRequest.Password);
			await _userService.AddAsync(userModel);
			return new UserRegistrationResponse() { IsSuccess = true };
		}
	}
}
