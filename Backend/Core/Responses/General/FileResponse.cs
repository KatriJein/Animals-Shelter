using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.General
{
	public class FileResponse
	{
		public string Type { get; set; }
		public string Link { get; set; }
		public DateTime? UploadTime { get; set; }
	}
}
