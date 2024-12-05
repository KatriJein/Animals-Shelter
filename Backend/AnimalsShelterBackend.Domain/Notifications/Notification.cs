using AnimalsShelterBackend.Domain.ShelterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Notifications
{
	public class Notification
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public bool Clickable { get; set; }
		public string? LinkTo { get; set; }
		public DateTime SentAt { get; set; }
		public List<User> Users { get; set; } = new List<User>();
	}
}
