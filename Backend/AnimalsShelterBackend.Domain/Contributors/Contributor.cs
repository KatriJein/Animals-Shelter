using Core.Enums.Contributors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.Contributors
{
	public class Contributor
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ContributorType ContributorType { get; set; }
	}
}
