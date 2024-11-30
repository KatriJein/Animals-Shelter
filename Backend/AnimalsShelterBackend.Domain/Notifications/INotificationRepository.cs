using Core.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Notifications
{
	public interface INotificationRepository : IRepository<Notification>
	{
	}
}
