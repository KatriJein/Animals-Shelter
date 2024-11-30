using AnimalsShelterBackend.Domain.Notifications;
using Core.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure.Notifications
{
	public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
	{
		private readonly ShelterAppContext _context;

		public NotificationRepository(ShelterAppContext context) : base(context, context.Notifications)
		{
			_context = context;
		}

		public override async Task DeleteAsync(Guid id)
		{
			await _context.Notifications.Where(n => n.Id == id).ExecuteDeleteAsync();
		}

		public override IQueryable<Notification> GetAll()
		{
			return _context.Notifications;
		}
	}
}
