using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.ArticleViews
{
	public class View
	{
		public Guid Id { get; set; }
		public Guid ArticleId { get; set; }
		public Guid UserId { get; set; }
	}
}
