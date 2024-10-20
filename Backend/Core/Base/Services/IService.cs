using Core.Requests;
using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Services
{
	public interface IService<T> where T : class
	{
		Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
		Task<T?> GetByGuidAsync(Guid id, CancellationToken cancellationToken);
		Task DeleteAsync(Guid id);
		Task<CreateEntityResponse> AddAsync(T entity);
		Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request);
		Task SaveChangesAsync();
	}
}
