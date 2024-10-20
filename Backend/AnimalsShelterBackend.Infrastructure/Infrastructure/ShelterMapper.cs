using AnimalsShelterBackend.Domain.Animals;
using AutoMapper;
using Core.Requests.Animals;
using Core.Responses.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Infrastructure.Infrastructure
{
	public class ShelterMapper : Profile
	{
		public ShelterMapper() {
			MapAnimals();
		}

		private void MapAnimals()
		{
			CreateMap<Animal, AnimalFullResponse>()
				.ForMember(dest => dest.MainImageSrc, mOpt => mOpt.MapFrom(m => m.MainImageName))
				.ForMember(dest => dest.VaccinationsInfo, mOpt => mOpt.MapFrom(m => m.VaccinationsInfo.Split("|", StringSplitOptions.RemoveEmptyEntries)))
				.ForMember(dest => dest.SterilizationsInfo, mOpt => mOpt.MapFrom(m => m.SterilizationsInfo.Split("|", StringSplitOptions.RemoveEmptyEntries)))
				.ForMember(dest => dest.RequiredConditions, mOpt => mOpt.MapFrom(m => m.RequiredConditions.Split("|", StringSplitOptions.RemoveEmptyEntries)))
				.ForMember(dest => dest.BehaviourFeatures, mOpt => mOpt.MapFrom(m => m.BehaviourFeatures.Split("|", StringSplitOptions.RemoveEmptyEntries)))
				.ForMember(dest => dest.OwnerWishes, mOpt => mOpt.MapFrom(m => m.OwnerWishes.Split("|", StringSplitOptions.RemoveEmptyEntries)))
				.ForMember(dest => dest.ImagesSrc, mOpt => mOpt.MapFrom(m => m.ImagesNames.Split("|", StringSplitOptions.RemoveEmptyEntries)));

			CreateMap<CreateAnimalRequest, Animal>()
				.ForMember(dest => dest.MainImageName, mOpt => mOpt.MapFrom(m => m.Images[0].FileName))
				.ForMember(dest => dest.VaccinationsInfo, mOpt => mOpt.MapFrom(m => string.Join("|", m.VaccinationsInfo)))
				.ForMember(dest => dest.SterilizationsInfo, mOpt => mOpt.MapFrom(m => string.Join("|", m.SterilizationsInfo)))
				.ForMember(dest => dest.RequiredConditions, mOpt => mOpt.MapFrom(m => string.Join("|", m.RequiredConditions)))
				.ForMember(dest => dest.BehaviourFeatures, mOpt => mOpt.MapFrom(m => string.Join("|", m.BehaviourFeatures)))
				.ForMember(dest => dest.OwnerWishes, mOpt => mOpt.MapFrom(m => string.Join("|", m.OwnerWishes)))
				.ForMember(dest => dest.ImagesNames, mOpt => mOpt.MapFrom(m => string.Join("|", m.Images.Skip(1).Select(i => i.FileName))));
		}
	}
}
