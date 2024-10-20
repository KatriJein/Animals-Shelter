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
		private readonly IImageService _imageService;

		public ImagesController(IImageService imageService)
		{
			_imageService = imageService;
		}

		/// <summary>
		/// Получение изображения животного по названию файла
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("animals/{fileName}")]
		public async Task<IActionResult> GetAnimalPicture([FromRoute] string fileName)
		{
			return Ok(await _imageService.GetImage(Const.AnimalsBucketName, fileName));
		}
	}
}
