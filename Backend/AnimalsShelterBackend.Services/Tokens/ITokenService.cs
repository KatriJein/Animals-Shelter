using AnimalsShelterBackend.Domain.ShelterUser;
using AnimalsShelterBackend.Domain.Tokens;
using Core.Responses.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.RefreshTokens
{
	public interface ITokenService
	{
		Task RemoveByUserIdAsync(Guid userId);
		Task<CreateOrUpdateTokensResponse> CreateOrUpdateTokensAsync(User user);
		Task<CreateOrUpdateTokensResponse> RefreshTokensAsync(Guid userId);
		Task<RefreshToken?> GetByUserIdAsync(Guid userId);
		Task SaveChangesAsync();
	}
}
