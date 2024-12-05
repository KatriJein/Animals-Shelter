using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Views
{
	public record CountViewRequest(Guid ArticleId, Guid UserId);
}
