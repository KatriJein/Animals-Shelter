
using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Enums.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Articles
{
	public class Article
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public Tag Tag { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
		public string BodyMarkDown { get; set; }
		public string MainImageSrc { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }
		[Column("authorId")]
		public Guid UserId { get; set; }
		public User? User { get; set; }
	}
}
