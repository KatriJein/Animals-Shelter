using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Articles
{
	public class CreateArticleResponse : BaseResponse
	{
		public Guid? Id { get; set; }
	}
}
