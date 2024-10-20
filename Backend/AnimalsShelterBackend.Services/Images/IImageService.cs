using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Images
{
	public interface IImageService
	{
		Task<bool> TryGetBucket(string bucket);
		Task DeleteImages(string bucket, List<string> fileNames);
		Task UploadImages(string bucket, List<IFormFile> files);
		Task<byte[]> GetImage(string bucket, string fileName);
	}
}
