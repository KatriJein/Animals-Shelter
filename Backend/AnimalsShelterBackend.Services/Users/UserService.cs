using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.ShelterUser.Repositories;
using AnimalsShelterBackend.Services.Images;
using Core.Base;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Requests;
using Core.Requests.Users;
using Core.Responses.General;
using Core.Responses.Users;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users
{
	public class UserService : BaseService<User>, IUserService
	{
		private readonly IUserRepository _repository;
		private readonly IFileService _fileService;
		private readonly string _hostLink;

		public UserService(IUserRepository repository, IFileService fileService, IConfiguration config) : base(repository)
		{
			_repository = repository;
			_fileService = fileService;
			_hostLink = config[Const.MinioLink];
		}

		public async Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken)
		{
			var user = (await GetAllAsync(cancellationToken))
				.Where(u => u.Email == login.ToLower() || UserUtils.CheckPhone(login, u.Phone))
				.FirstOrDefault();
			return user;
		}

		public override async Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var user = await GetByGuidAsync(id, CancellationToken.None);
			if (user == null) return new UserUpdateResponse() { IsSuccess = false, Message = "Пользователь не существует" };
			var userUpdateRequest = (UpdateUserRequest)request;
			var existingUser = (await GetAllAsync(CancellationToken.None))
				.Where(u => (userUpdateRequest.Email != null && u.Email == userUpdateRequest.Email.ToLower() ||
				 userUpdateRequest.Phone != null && UserUtils.CheckPhone(userUpdateRequest.Phone, u.Phone)) && u.Id != user.Id)
				.FirstOrDefault();
			if (existingUser != null)
				return new UserUpdateResponse() { IsSuccess = false, Message = "На данную почту или телефон уже зарегистрирован аккаунт" };
			user.Name = userUpdateRequest.Name ?? user.Name;
			user.Surname = userUpdateRequest.Surname ?? user.Surname;
			if (userUpdateRequest.Email != null)
			{
				var isMatch = Const.EmailRegex.IsMatch(userUpdateRequest.Email);
				if (isMatch) user.Email = userUpdateRequest.Email;
				else return new UserUpdateResponse() { IsSuccess = false, Message = "Некорректный адрес почты" };
			}
			if (userUpdateRequest.Phone != null)
			{
				var converted = UserUtils.TryConvertPhoneInputToEight(userUpdateRequest.Phone, out long phone);
				if (!converted) return new UserUpdateResponse() { IsSuccess = false, Message = "Некорректный номер телефона" };
				user.Phone = phone;
			}
			return await base.UpdateAsync(id, request);
		}

		public async Task<UserAvatarUpdateResponse> UpdateUserAvatarAsync(Guid id, IFormFile? avatar)
		{
			var user = await GetByGuidAsync(id, CancellationToken.None);
			if (user == null) return new UserAvatarUpdateResponse() { IsSuccess = false, Message = "Пользователь не существует" };
			if (avatar != null)
			{
				if (user.AvatarSrc != null) await _fileService.DeleteFiles(Const.UsersBucketName, [user.AvatarSrc]);
				user.AvatarSrc = FilesUtils.GenerateFileSources([avatar], _hostLink, Const.UsersBucketName)[0];
				await _fileService.UploadFiles(Const.UsersBucketName, [avatar], [user.AvatarSrc]);
			}
			else user.AvatarSrc = null;
			await SaveChangesAsync();
			return new UserAvatarUpdateResponse() { IsSuccess = true };
		}

		public override async Task DeleteAsync(Guid id)
		{
			var user = await GetByGuidAsync(id, CancellationToken.None);
			if (user != null && user.AvatarSrc != null) await _fileService.DeleteFiles(Const.UsersBucketName, [user.AvatarSrc]);
			await base.DeleteAsync(id);
		}

		public async Task LoadUserArticlesAsync(User user, CancellationToken cancellationToken = default)
		{
			await _repository.LoadUserArticlesAsync(user, cancellationToken);
		}

		public async Task LoadUserFavouriteAnimalsAsync(User user, CancellationToken cancellationToken = default)
		{
			await _repository.LoadUserFavouriteAnimalsAsync(user, cancellationToken);
		}

		public async Task<User?> FindAdminUserAsync()
		{
			return (await GetAllAsync(CancellationToken.None))
				.Where(u => u.IsAdmin)
				.FirstOrDefault();
		}

		public async Task<UpdatePasswordResponse> UpdatePasswordAsync(Guid userId, UpdatePasswordRequest updatePasswordRequest)
		{
			var user = await GetByGuidAsync(userId, CancellationToken.None);
			if (user == null) return new UpdatePasswordResponse() { IsSuccess = false, Message = "Пользователь не существует" };
			if (!UserUtils.ArePasswordsEqual(updatePasswordRequest.OldPassword, user.PasswordHash))
				return new UpdatePasswordResponse() { IsSuccess = false, Message = "Текущий пароль не верен" };
			var newPasswordHash = UserUtils.HashPassword(updatePasswordRequest.NewPassword);
			user.PasswordHash = newPasswordHash;
			await SaveChangesAsync();
			return new UpdatePasswordResponse() { IsSuccess = true };
		}
	}
}
