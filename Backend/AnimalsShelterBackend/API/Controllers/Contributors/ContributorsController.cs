using AnimalsShelterBackend.Domain.Contributors;
using AnimalsShelterBackend.Services.Contributors;
using AutoMapper;
using Core.Requests.Contributors;
using Core.Responses.Contributors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Contributors
{
	[Route("api/contributors")]
	[ApiController]
	public class ContributorsController : ControllerBase
	{
		private readonly IContributorsService _contributorsService;
		private readonly IMapper _mapper;

		public ContributorsController(IContributorsService contributorsService, IMapper mapper)
		{
			_contributorsService = contributorsService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить всех спонсоров и волонтеров
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			var contributors = await _contributorsService.GetAllAsync(cancellationToken);
			if (contributors.Count == 0) return NoContent();
			return Ok(_mapper.Map<List<ContributorResponse>>(contributors));
		}

		/// <summary>
		/// Дбоавить спонсора или волонтера
		/// </summary>
		/// <param name="createContributorRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CreateAsync([FromBody] CreateContributorRequest createContributorRequest)
		{
			var contributor = _mapper.Map<Contributor>(createContributorRequest);
			var response = await _contributorsService.AddAsync(contributor);
			return Ok(response);
		}

		/// <summary>
		/// Обновить спонсора или волонтера
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateContributorRequest"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateContributorRequest updateContributorRequest)
		{
			var response = await _contributorsService.UpdateAsync(id, updateContributorRequest);
			if (!response.IsSuccess) return BadRequest(response);
			return Ok();
		}

		/// <summary>
		/// Удалить спонсора или волонтера
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			await _contributorsService.DeleteAsync(id);
			return Ok();
		}
	}
}
