using AnimalsShelterBackend.Domain.Feedbacks;
using AnimalsShelterBackend.Services.Feedbacks;
using AnimalsShelterBackend.Services.Users;
using AutoMapper;
using Core.Requests.Feedbacks;
using Core.Responses.Feedbacks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Feedbacks
{
	[Route("api/feedbacks")]
	[ApiController]
	public class FeedbacksController : ControllerBase
	{
		private readonly IFeedbacksService _feedbacksService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public FeedbacksController(IFeedbacksService feedbacksService, IUserService userService, IMapper mapper)
		{
			_feedbacksService = feedbacksService;
			_userService = userService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить все отзывы
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("")]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			var feedbacks = await _feedbacksService.GetAllAsync(cancellationToken);
			if (feedbacks.Count == 0) return NoContent();
			var mapped = _mapper.Map<List<FeedbackResponse>>(feedbacks);
			return Ok(mapped);
		}

		/// <summary>
		/// Получить отдельный отзыв
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid id, CancellationToken cancellationToken)
		{
			var feedback = await _feedbacksService.GetByGuidAsync(id, cancellationToken);
			if (feedback == null) return NotFound();
			return Ok(_mapper.Map<FeedbackResponse>(feedback));
		}

		/// <summary>
		/// Создать отзыв
		/// </summary>
		/// <param name="createFeedbackRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CreateAsync([FromBody] CreateFeedbackRequest createFeedbackRequest)
		{
			var feedback = _mapper.Map<Feedback>(createFeedbackRequest);
			var result = await _feedbacksService.AddAsync(feedback, _userService);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok(result.EntityId);
		}

		/// <summary>
		/// Обновить данные отзыва
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateFeedbackRequest"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateFeedbackRequest updateFeedbackRequest)
		{
			var result = await _feedbacksService.UpdateAsync(id, updateFeedbackRequest);
			if (!result.IsSuccess) return BadRequest(result.Message);
			return Ok();
		}

		/// <summary>
		/// Удалить отзыв
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			await _feedbacksService.DeleteAsync(id);
			return Ok();
		}
	}
}
