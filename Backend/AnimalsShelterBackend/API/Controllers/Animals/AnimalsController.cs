using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Services.Animals;
using AnimalsShelterBackend.Services.Images;
using AnimalsShelterBackend.Services.Users;
using AutoMapper;
using Core.Base.Services;
using Core.Constants;
using Core.Queries;
using Core.Requests.Animals;
using Core.Responses.Animals;
using Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsShelterBackend.API.Controllers.Animals
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AnimalsController(IAnimalsService animalsService, IUserService userService, IMapper mapper)
        {
            _animalsService = animalsService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех животных приюта
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] AnimalsQuery animalsQuery, CancellationToken cancellationToken)
        {
			var user = await _userService.GetByGuidAsync(animalsQuery.UserId, cancellationToken);
			if (animalsQuery.UserId == Guid.Empty || user == null)
            {
				var animals = await _animalsService.GetAllAsync(cancellationToken);
                CommonUtils.AddHeaderToResponse(HttpContext, Const.CountHeader, animals.Count);
                return Ok(_mapper.Map<List<AnimalFullResponse>>(animals));
			}
            await _userService.LoadUserFavouriteAnimalsAsync(user, cancellationToken);
            var response = await _animalsService.GetAllWithIsFavouriteMarkAsync(user.FavouriteAnimals, _mapper, cancellationToken);
			CommonUtils.AddHeaderToResponse(HttpContext, Const.CountHeader, response.Count);
			return Ok(response);
        }

        /// <summary>
        /// Получение отдельного животного по Guid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var animal = await _animalsService.GetByGuidAsync(id, cancellationToken);
            if (animal == null) return NoContent();
            var response = _mapper.Map<AnimalFullResponse>(animal);
            return Ok(response);
        }

        /// <summary>
        /// Удаление карточки животного по Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveByGuidAsync([FromRoute] Guid id)
        {
            await _animalsService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Добавление нового животного
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAnimalAsync([FromForm] CreateAnimalRequest createAnimalRequest)
        {
            var animal = _mapper.Map<Animal>(createAnimalRequest);
            var createdResponse = await _animalsService.AddAsync(animal, createAnimalRequest.Images);
            return Ok(createdResponse);
        }

        /// <summary>
        /// Обновление карточки животного по Guid
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateAnimalAsync([FromRoute] Guid id, [FromForm] UpdateAnimalRequest updateAnimalRequest)
        {
            var response = await _animalsService.UpdateAsync(id, updateAnimalRequest);
            if (response.IsSuccess) return Ok();
            return BadRequest(response.Message);
        }
    }
}
