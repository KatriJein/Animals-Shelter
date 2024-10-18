using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class
	{
		private readonly DbSet<T> _dbSet;
		private readonly DbContext _context;

		public BaseRepository(DbContext context, DbSet<T> dbSet)
		{
			_dbSet = dbSet;
			_context = context;
		}

		public virtual async Task<Guid> AddAsync(T entity)
		{
			var created = await _dbSet.AddAsync(entity);
			return (Guid)created.Property("Id").CurrentValue;
		}

		public abstract Task DeleteAsync(Guid id);

		public abstract IQueryable<T> GetAll();

		public virtual async Task<T> GetByGuidAsync(Guid id, CancellationToken cancellationToken)
		{
			return await _dbSet.FindAsync(id, cancellationToken);
		}

		public virtual async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public virtual async Task UpdateAsync()
		{
			await SaveChangesAsync();
		}
	}
}
