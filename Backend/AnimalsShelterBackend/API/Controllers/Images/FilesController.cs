using AnimalsShelterBackend.Services.Images;
using Core.Constants;
using Core.Requests;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Images
{
	[Route("api/files")]
	[ApiController]
	public class FilesController : ControllerBase
	{
		private readonly IFileService _fileService;
		private readonly string _host;

		public FilesController(IFileService imageService, IConfiguration config)
		{
			_fileService = imageService;
			_host = config[Const.MinioLink];
		}

		/// <summary>
		/// Получение файла животного
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("animals/{fileName}")]
		public async Task<IActionResult> GetAnimalFile([FromRoute] string fileName)
		{
			return Ok(await _fileService.GetFile(Const.AnimalsBucketName, fileName));
		}

		/// <summary>
		/// Загрузить группу файлов (картинки и т.п.) 
		/// </summary>
		/// <param name="filesRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("upload")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UploadFiles([FromForm] FilesRequest filesRequest)
		{
			var filePaths = FilesUtils.GenerateFileSources(filesRequest.Files, _host, Const.NewsArticlesBucketName);
			await _fileService.UploadFiles(Const.NewsArticlesBucketName, filesRequest.Files, filePaths);
			return Ok();
		}
	}
}
