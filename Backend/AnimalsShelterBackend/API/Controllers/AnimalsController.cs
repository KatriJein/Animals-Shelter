using AnimalsShelterBackend.Domain.Animals;
using AutoMapper;
using Core.Base.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers
{
	[Route("api/animals")]
	[ApiController]
	public class AnimalsController : ControllerBase
	{
		private readonly IService<Animal> _animalsService;
		private readonly IMapper _mapper;

		public AnimalsController(IService<Animal> animalsService, IMapper mapper)
		{
			_animalsService = animalsService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("/all")]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			var entities = await _animalsService.GetAllAsync(cancellationToken);
			return Ok(entities);
		}
	}
}
