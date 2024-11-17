using AnimalsShelterBackend.Domain.ShelterUser;
using AutoMapper;
using Core.Requests.Users;
using Core.Responses.General;
using Core.Responses.Users;
using Core.Responses.Users.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.AuthServices
{
	public interface IAuthService
	{
		Task<BaseResponse> FinishRegistrationAsync(Guid id, UpdateUserRequest updateUserRequest);
		Task<UserRegistrationResponse> RegisterAsync(UserRegisterRequest userRegisterRequest, bool createAdmin=false);
		Task<UserAuthenthicationResponse> AuthenthicateAsync(UserLoginRequest userLoginRequest, IMapper mapper, CancellationToken cancellationToken,
			bool isAutoAuthenthicate=false);
	}
}
