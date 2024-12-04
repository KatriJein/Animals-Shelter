using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Feedbacks
{
	public record CreateFeedbackRequest(Guid UserId, string Text, int Rating);
}
