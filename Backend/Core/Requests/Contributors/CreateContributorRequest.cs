using Core.Enums.Contributors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Contributors
{
	public record CreateContributorRequest(string Name, ContributorType ContributorType);
}
