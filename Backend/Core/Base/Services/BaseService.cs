using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Services
{
	public abstract class BaseService<T> : IService<T> where T : class
	{
		private readonly IRepository<T> _repository;

		public BaseService(IRepository<T> repository)
		{
			_repository = repository;
		}

		public virtual async Task<BaseServiceResponse> AddAsync(T entity)
		{
			var createdId = await _repository.AddAsync(entity);
			return new BaseServiceResponse() { Id = createdId };
		}

		public virtual async Task DeleteAsync(Guid id)
		{
			await _repository.DeleteAsync(id);
		}

		public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _repository.GetAll().ToListAsync(cancellationToken);
		}

		public virtual async Task<T> GetByGuidAsync(Guid id, CancellationToken cancellationToken)
		{
			return await _repository.GetByGuidAsync(id, cancellationToken);
		}

		public virtual async Task SaveChangesAsync()
		{
			await _repository.SaveChangesAsync();
		}

		public virtual async Task UpdateAsync()
		{
			await _repository.UpdateAsync();
		}
	}
}
