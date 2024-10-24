using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Services.Images;
using Core.Base;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Requests;
using Core.Requests.Animals;
using Core.Responses.General;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AnimalsShelterBackend.Services.Animals
{
	public class AnimalsService : BaseService<Animal>, IAnimalsService
	{
		private readonly IRepository<Animal> _animalsRepository;
		private readonly IFileService _fileService;
		private readonly string _hostLink;

		public AnimalsService(IRepository<Animal> animalsRepository, IFileService fileService, IConfiguration config) : base(animalsRepository)
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
			animal.Age = animalRequest.Age;
			animal.Sex = animalRequest.Sex;
			animal.Wool = animalRequest.Wool;
			animal.Size = animalRequest.Size;
			animal.TemperFeatures = string.Join(Const.Separator, animalRequest.TemperFeatures.Select(e => ((int)e).ToString()));
			animal.Color = animalRequest.Color;
			animal.Name = animalRequest.Name;
			animal.Breed = animalRequest.Breed;
			animal.Description = animalRequest.Description;
			animal.HealthConditions = string.Join(Const.Separator, animalRequest.HealthConditions.Select(e => ((int)e).ToString()));
			animal.LivingCondition = animalRequest.LivingCondition;
			animal.ReceiptDate = animalRequest.ReceiptDate;
			var fileSources = AssignAnimalFileSources(animal, animalRequest.Images);
			var response = await base.UpdateAsync(id, animalRequest);
			await _fileService.UploadFiles(Const.AnimalsBucketName, animalRequest.Images, fileSources);
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
	}
}
