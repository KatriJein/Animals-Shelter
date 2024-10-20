using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace AnimalsShelterBackend.Services.Images
{
	public class ImageService : IImageService
	{
		private readonly IMinioClientFactory _minioClientFactory;
		private readonly IMinioClient _minioClient;
		private readonly ILogger _logger;

		public ImageService(IMinioClientFactory minioClientFactory, ILogger logger)
		{
			_minioClientFactory = minioClientFactory;
			_minioClient = _minioClientFactory.CreateClient();
			_logger = logger;
		}

		public async Task DeleteImages(string bucket, List<string> fileNames)
		{
			var doesExists = await TryGetBucket(bucket);
			if (!doesExists) return;
			foreach (var file in fileNames)
			{
				try
				{
					var removeArgs = new RemoveObjectArgs().WithBucket(bucket).WithObject(file);
					await _minioClient.RemoveObjectAsync(removeArgs);
				}
				catch (InvalidObjectNameException)
				{
					_logger.Error("Не удалось удалить файл {file} в корзине {bucket})", file, bucket);
				}
			}
		}

		public async Task<byte[]> GetImage(string bucket, string fileName)
		{
			var bucketExists = await TryGetBucket(bucket);
			if (!bucketExists) return [];
			try
			{
				using (var stream = new MemoryStream())
				{
					var getObjectArgs = new GetObjectArgs().WithBucket(bucket).WithObject(fileName)
						.WithCallbackStream(s => s.CopyTo(stream));
					var data = await _minioClient.GetObjectAsync(getObjectArgs);
					return stream.ToArray();
				}
			}
			catch
			{
				_logger.Error("Не удалось получить изображение {fileName} из корзины {bucket}", fileName, bucket);
				return [];
			}
		}

		public async Task<bool> TryGetBucket(string bucket)
		{
			var existsArgs = new BucketExistsArgs().WithBucket(bucket);
			return await _minioClient.BucketExistsAsync(existsArgs);
		}

		public async Task UploadImages(string bucket, List<IFormFile> files)
		{
			var bucketExists = await TryGetBucket(bucket);
			if (!bucketExists)
			{
				var makeBucketArgs = new MakeBucketArgs().WithBucket(bucket);
				await _minioClient.MakeBucketAsync(makeBucketArgs);
			}
			foreach (var file in files)
			{
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					stream.Position = 0;
					var putObjectArgs = new PutObjectArgs().WithBucket(bucket).WithObject(file.FileName)
					.WithContentType(file.ContentType).WithStreamData(stream).WithObjectSize(stream.Length);
					await _minioClient.PutObjectAsync(putObjectArgs);
				}
			}
		}
	}
}
