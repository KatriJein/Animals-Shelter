using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Animals.Repositories;
using AnimalsShelterBackend.Services.Images;
using AutoMapper;
using Core.Base;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Enums.Animals;
using Core.Requests;
using Core.Requests.Animals;
using Core.Responses.Animals;
using Core.Responses.General;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AnimalsShelterBackend.Services.Animals
{
	public class AnimalsService : BaseService<Animal>, IAnimalsService
	{
		private readonly IAnimalsRepository _animalsRepository;
		private readonly IFileService _fileService;
		private readonly string _hostLink;

		public AnimalsService(IAnimalsRepository animalsRepository, IFileService fileService, IConfiguration config) : base(animalsRepository)
		{
			_animalsRepository = animalsRepository;
			_fileService = fileService;
			_hostLink = config[Const.MinioLink];
		}

		public override async Task<BaseResponse> UpdateAsync(Guid id, IUpdateRequest request)
		{
			var animalRequest = (UpdateAnimalRequest)request;
			var animal = await GetByGuidAsync(id, CancellationToken.None);
			if (animal == null) return new UpdateResponse()
			{
				IsSuccess = false,
				Message = "Попытка обновить несуществующую карточку животного"
			};
			animal.Age = animalRequest.Age ?? animal.Age;
			animal.Sex = animalRequest.Sex ?? animal.Sex;
			animal.Wool = animalRequest.Wool ?? animal.Wool;
			animal.Size = animalRequest.Size ?? animal.Size;
			if (!CommonUtils.IsNullable(animalRequest.TemperFeatures))
				animal.TemperFeatures = string.Join(Const.Separator, animalRequest.TemperFeatures.Select(e => ((int)e).ToString()));
			animal.Color = animalRequest.Color ?? animal.Color;
			animal.Name = animalRequest.Name ?? animal.Name;
			animal.Breed = animalRequest.Breed ?? animal.Breed;
			animal.Description = animalRequest.Description ?? animal.Description;
			animal.ShortDescription = animalRequest.ShortDescription ?? animal.Description;
			if (!CommonUtils.IsNullable(animalRequest.HealthConditions))
				animal.HealthConditions = string.Join(Const.Separator, animalRequest.HealthConditions.Select(e => ((int)e).ToString()));
			animal.LivingCondition = animalRequest.LivingCondition ?? animal.LivingCondition;
			animal.ReceiptDate = animalRequest.ReceiptDate ?? animal.ReceiptDate;
			if (animalRequest.Images != null)
			{
				var fileSources = AssignAnimalFileSources(animal, animalRequest.Images);
				await _fileService.UploadFiles(Const.AnimalsBucketName, animalRequest.Images, fileSources);
			}
			var response = await base.UpdateAsync(id, animalRequest);
			return response;
		}

		public override async Task DeleteAsync(Guid id)
		{
			var entity = await GetByGuidAsync(id, CancellationToken.None);
			if (entity == null) return;
			var fileSources = entity.ImagesSources.Split(Const.Separator, StringSplitOptions.RemoveEmptyEntries).ToList();
			fileSources.Add(entity.MainImageSource);
			await _fileService.DeleteFiles(Const.AnimalsBucketName, fileSources);
			await base.DeleteAsync(id);
		}

		public async Task<CreateEntityResponse> AddAsync(Animal animal, List<IFormFile> files)
		{

			var fileSources = AssignAnimalFileSources(animal, files);
			var response = await AddAsync(animal);
			await _fileService.UploadFiles(Const.AnimalsBucketName, files, fileSources);
			return response;
		}

		private List<string> AssignAnimalFileSources(Animal animal, List<IFormFile> files)
		{
			var fileSources = FilesUtils.GenerateFileSources(files, _hostLink, Const.AnimalsBucketName);
			animal.MainImageSource = fileSources[0];
			animal.ImagesSources = string.Join(Const.Separator, fileSources.Skip(1));
			return fileSources;
		}

		public async Task LoadUsersForAnimalAsync(Animal animal)
		{
			await _animalsRepository.LoadUsersForAnimalAsync(animal);
		}

		public async Task<List<AnimalFullResponse>> GetAllWithIsFavouriteMarkAsync(List<Animal> favourites, IMapper mapper, CancellationToken cancellationToken)
		{
			var animals = await GetAllAsync(cancellationToken);
			var mappedAnimals = new List<AnimalFullResponse>();
			foreach (var animal in animals)
			{
				var mappedAnimal = mapper.Map<AnimalFullResponse>(animal);
				mappedAnimal.IsFavourite = favourites.Any(fv => fv.Id == animal.Id);
				mappedAnimals.Add(mappedAnimal);
			}
			return mappedAnimals;
		}
	}
}
