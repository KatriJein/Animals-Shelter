using Core.Responses.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Articles
{
	public class ArticlesFilesResponse() : BaseResponse
	{
		public List<FileResponse> Files { get; set; }
	}
}
