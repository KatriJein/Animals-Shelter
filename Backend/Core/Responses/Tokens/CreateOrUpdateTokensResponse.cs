using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Tokens
{
	public class CreateOrUpdateTokensResponse: BaseResponse
	{
		public string AccessToken { get; set; }
	}
}
