using AnimalsShelterBackend.Services.Views;
using Core.Requests.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Views
{
	[Route("api/views")]
	[ApiController]
	public class ViewsController : ControllerBase
	{
		private readonly IViewsService _viewsService;

		public ViewsController(IViewsService viewsService)
		{
			_viewsService = viewsService;
		}

		/// <summary>
		/// Засчитать просмотр данной статьи для данного пользователя
		/// </summary>
		/// <param name="countViewRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CountViewAsync([FromBody] CountViewRequest countViewRequest)
		{
			var result = await _viewsService.CountViewAsync(countViewRequest);
			if (!result.IsSuccess) return BadRequest(result);
			return Ok(result);
		}
	}
}
