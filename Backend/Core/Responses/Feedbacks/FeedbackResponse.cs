using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Feedbacks
{
	public class FeedbackResponse
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
