using AnimalsShelterBackend.Domain.ArticleViews;
using AnimalsShelterBackend.Domain.ArticleViews.Repositories;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Views
{
	public class ViewsRepository : BaseRepository<View>, IViewsRepository
	{
		private readonly ShelterAppContext _context;

		public ViewsRepository(ShelterAppContext context): base(context, context.Views)
		{
			_context = context;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _context.Views.Where(v => v.Id == id).ExecuteDeleteAsync();
		}

		public Task<View?> FindByArticleIdAndUserIdAsync(Guid articleId, Guid userId, CancellationToken cancellationToken)
		{
			return _context.Views.Where(v => v.ArticleId == articleId && v.UserId == userId).FirstOrDefaultAsync(cancellationToken);
		}

		public override IQueryable<View> GetAll()
		{
			return _context.Views;
		}
	}
}
