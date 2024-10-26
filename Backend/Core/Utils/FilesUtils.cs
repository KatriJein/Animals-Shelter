using Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class FilesUtils
	{
		public static string GenerateRandomNameForFile(string file)
		{
			var extension = Path.GetExtension(file);
			var fileName = Path.GetFileNameWithoutExtension(file);
			return fileName + DateTime.Now.Ticks + extension;
		}

		public static List<string> GenerateFileSources(List<IFormFile> files, string hostName, string bucketName)
		{
			return files.Select(f => $"{hostName}/{bucketName}/{GenerateRandomNameForFile(f.FileName)}")
				.ToList();
		}

		public static List<string> GetFileNamesFromSources(List<string> fileSources)
		{
			return fileSources.Select(fs => fs.Split(Const.UrlSeparator).Last()).ToList();
		}
	}
}
