using Core.Base;
using Core.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Tokens.Repositories
{
	public interface IRefreshTokenRepository
	{
		Task<RefreshToken?> GetByUserIdAsync(Guid userId);
		Task<Guid> AddAsync(RefreshToken refreshToken);
		Task RemoveByUserIdAsync(Guid userId);
		Task SaveChangesAsync();
	}
}
