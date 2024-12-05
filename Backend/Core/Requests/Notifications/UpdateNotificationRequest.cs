using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Notifications
{
	public record UpdateNotificationRequest(string Title, string? Description, bool Clickable, string? LinkTo) : IUpdateRequest;
}
