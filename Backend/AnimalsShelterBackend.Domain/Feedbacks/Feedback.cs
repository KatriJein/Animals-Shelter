using AnimalsShelterBackend.Domain.ShelterUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Feedbacks
{
	public class Feedback
	{
		public Guid Id { get; set; }
		public string UserName { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
