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
using Microsoft.Extensions.Configuration;
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
				.Where(u => u.Email == login.ToLower() || u.Phone == UserUtils.ConvertPhoneInputToEight(login))
				.FirstOrDefault();
			return user;
		}

		public override async Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var user = await GetByGuidAsync(id, CancellationToken.None);
			if (user == null) return new UserUpdateResponse() { IsSuccess = false, Message = "Пользователь не существует" };
			var userUpdateRequest = (UpdateUserRequest)request;
			var existingUser = (await GetAllAsync(CancellationToken.None))
				.Where((u => u.Email == userUpdateRequest.Email.ToLower() ||
				u.Phone == UserUtils.ConvertPhoneInputToEight(userUpdateRequest.Phone) && u.Id != user.Id))
				.FirstOrDefault();
			if (existingUser != null)
				return new UserUpdateResponse() { IsSuccess = false, Message = "На данную почту или телефон уже зарегистрирован аккаунт" };
			user.Name = userUpdateRequest.Name;
			user.Surname = userUpdateRequest.Surname;
			user.Email = userUpdateRequest.Email;
			user.Phone = UserUtils.ConvertPhoneInputToEight(userUpdateRequest.Phone);
			if (userUpdateRequest.Avatar != null)
			{
				if (user.AvatarSrc != null) await _fileService.DeleteFiles(Const.UsersBucketName, [user.AvatarSrc]);
				user.AvatarSrc = FilesUtils.GenerateFileSources([userUpdateRequest.Avatar], _hostLink, Const.UsersBucketName)[0];
				await _fileService.UploadFiles(Const.UsersBucketName, [userUpdateRequest.Avatar], [user.AvatarSrc]);
			}
			else user.AvatarSrc = null;
			return await base.UpdateAsync(id, request);
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
	}
}
