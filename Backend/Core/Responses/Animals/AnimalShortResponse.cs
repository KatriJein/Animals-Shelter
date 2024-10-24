using Core.Enums.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Animals
{
	public class AnimalShortResponse
	{
		public Guid Id { get; set; }
		public int Age { get; set; }
		public string Name { get; set; }
		public string Breed { get; set; }
		public string MainImageSrc { get; set; }
		public Sex Sex { get; set; }
	}
}
