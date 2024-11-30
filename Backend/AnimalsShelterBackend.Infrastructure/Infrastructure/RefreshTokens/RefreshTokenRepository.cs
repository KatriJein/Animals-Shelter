using AnimalsShelterBackend.Domain.Tokens;
using AnimalsShelterBackend.Domain.Tokens.Repositories;
using Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.RefreshTokens
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly ShelterAppContext _shelterAppContext;

		public RefreshTokenRepository(ShelterAppContext shelterAppContext)
		{
			_shelterAppContext = shelterAppContext;
		}

		public async Task<Guid> AddAsync(RefreshToken refreshToken)
		{
			var entity = await _shelterAppContext.RefreshTokens.AddAsync(refreshToken);
			await SaveChangesAsync();
			return (Guid)entity.Property("Id").CurrentValue;
		}

		public async Task<RefreshToken?> GetByUserIdAsync(Guid userId)
		{
			return await _shelterAppContext.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == userId);
		}

		public async Task RemoveByUserIdAsync(Guid userId)
		{
			var entity = await GetByUserIdAsync(userId);
			if (entity != null)
				_shelterAppContext.RefreshTokens.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _shelterAppContext.SaveChangesAsync();
		}
	}
}
