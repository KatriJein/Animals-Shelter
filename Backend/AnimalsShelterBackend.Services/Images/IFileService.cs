using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Images
{
	public interface IFileService
	{
		Task<bool> TryGetBucket(string bucket);
		Task DeleteFiles(string bucket, List<string> fileSources);
		Task UploadFiles(string bucket, List<IFormFile> files, List<string> fileSources);
		Task<byte[]> GetFile(string bucket, string fileName);
	}
}
