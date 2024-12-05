using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Notifications
{
	public class GetNotificationsResponse : BaseResponse
	{
		public List<NotificationResponse>? Notifications { get; set; }
	}
}
