using Core.Enums.Articles;
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
		public Tag Tag { get; set; }
		public Category Category { get; set; }
		public int ViewsCount { get; set; }
		public string Description { get; set; }
		public string MainImageSrc { get; set; }
		public DateTime LastUpdatedAt { get; set; }
	}
}
