using AnimalsShelterBackend.Domain.Articles;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Articles.Repositories
{
	internal class ArticleRepository : BaseRepository<Article>
	{
		private readonly ShelterAppContext _context;

		public ArticleRepository(ShelterAppContext context) : base(context, context.Articles)
		{
			_context = context;
		}

		public override Task DeleteAsync(Guid id)
		{
			return _context.Articles.Where(a => a.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<Article> GetAll()
		{
			return _context.Articles;
		}

		public override async Task<Article?> GetByGuidAsync(Guid id, CancellationToken cancellationToken)
		{
			return await _context.Articles.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
		}
	}
}
