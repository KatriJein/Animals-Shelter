using Core.Enums.Articles;
using Core.Responses.Users.Author;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Articles
{
	public class ArticleFullResponse
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public Tag Tag { get; set; }
		public string Description { get; set; }
		public string BodyMarkDown { get; set; }
		public string MainImageSrc { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }
		public Guid UserId { get; set; }
		public Author Author { get; set; }
	}
}
