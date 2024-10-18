using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Repositories
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		Task<T> GetByGuidAsync(Guid id, CancellationToken cancellationToken);
		Task DeleteAsync(Guid id);
		Task<Guid> AddAsync(T entity);
		Task UpdateAsync();
		Task SaveChangesAsync();
	}
}
