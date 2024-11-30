using Core.Enums.Contributors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Contributors
{
	public class ContributorResponse
	{
		public string Name { get; set; }
		public ContributorType ContributorType { get; set; }
	}
}
