using Core.Enums.Contributors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Contributors
{
	public record UpdateContributorRequest(string? Name, ContributorType? ContributorType) : IUpdateRequest;
}
