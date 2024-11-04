using AnimalsShelterBackend.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.ShelterUser
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public List<Article> Articles { get; set; } = new List<Article>();
	}
}
