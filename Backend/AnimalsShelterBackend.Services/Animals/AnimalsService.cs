using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Services.Images;
using Core.Base.Repositories;
using Core.Base.Services;
using Core.Constants;
using Core.Requests;
using Core.Requests.Animals;
using Core.Responses.General;

namespace AnimalsShelterBackend.Services.Animals
{
	public class AnimalsService : BaseService<Animal>
	{
		private readonly IRepository<Animal> _animalsRepository;
		private readonly IImageService _imageService;

		public AnimalsService(IRepository<Animal> animalsRepository, IImageService imageService) : base(animalsRepository)
		{
			_animalsRepository = animalsRepository;
			_imageService = imageService;
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
			animal.TemperFeatures = string.Join("|", animalRequest.TemperFeatures.Select(e => ((int)e).ToString()));
			animal.Color = animalRequest.Color;
			animal.Name = animalRequest.Name;
			animal.Breed = animalRequest.Breed;
			animal.MainImageName = animalRequest.Images[0].FileName;
			animal.Description = animalRequest.Description;
			animal.HealthConditions = string.Join("|", animalRequest.HealthConditions.Select(e => ((int)e).ToString()));
			animal.LivingCondition = animalRequest.LivingCondition;
			animal.ReceiptDate = animalRequest.ReceiptDate;
			animal.ImagesNames = string.Join("|", animalRequest.Images.Skip(1).Select(i => i.FileName));
			return await base.UpdateAsync(id, animalRequest);
		}

		public override async Task DeleteAsync(Guid id)
		{
			var entity = await GetByGuidAsync(id, CancellationToken.None);
			if (entity == null) return;
			var fileNames = entity.ImagesNames.Split("|", StringSplitOptions.RemoveEmptyEntries).ToList();
			fileNames.Add(entity.MainImageName);
			await _imageService.DeleteImages(Const.AnimalsBucketName, fileNames);
			await base.DeleteAsync(id);
		}
	}
}
