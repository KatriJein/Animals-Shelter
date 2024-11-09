using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Articles
{
	public class ArticleShortResponse
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string MainImageSrc { get; set; }
		public DateTime LastUpdatedAt { get; set; }
	}
}
