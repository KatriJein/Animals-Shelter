using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Notifications
{
	public class NotificationResponse
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public bool Clickable { get; set; }
		public string? LinkTo { get; set; }
		public DateTime SentAt { get; set; }
	}
}
