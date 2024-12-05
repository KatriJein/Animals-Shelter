using Core.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.ArticleViews.Repositories
{
	public interface IViewsRepository : IRepository<View>
	{
		Task<View?> FindByArticleIdAndUserIdAsync(Guid articleId, Guid userId, CancellationToken cancellationToken);
	}
}
