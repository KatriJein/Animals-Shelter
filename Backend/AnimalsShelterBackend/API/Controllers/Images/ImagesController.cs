using AnimalsShelterBackend.Services.Images;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Images
{
	[Route("api/images")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IFileService _imageService;

		public ImagesController(IFileService imageService)
		{
			_imageService = imageService;
		}

		/// <summary>
		/// Получение файла животного
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("animals/{fileName}")]
		public async Task<IActionResult> GetAnimalFile([FromRoute] string fileName)
		{
			return Ok(await _imageService.GetFile(Const.AnimalsBucketName, fileName));
		}
	}
}
