using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Feedbacks
{
	public class CreateFeedbackResponse: BaseResponse
	{
		public Guid EntityId { get; set; }
	}
}
