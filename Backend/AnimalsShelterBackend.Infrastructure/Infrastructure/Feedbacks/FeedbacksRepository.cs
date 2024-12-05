using AnimalsShelterBackend.Domain.Feedbacks;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Feedbacks
{
	public class FeedbacksRepository: BaseRepository<Feedback>, IFeedbacksRepository
	{
		private readonly ShelterAppContext _context;

		public FeedbacksRepository(ShelterAppContext context): base(context, context.Feedbacks)
		{
			_context = context;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _context.Feedbacks.Where(f => f.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<Feedback> GetAll()
		{
			return _context.Feedbacks;
		}
	}
}
