using AnimalsShelterBackend.Domain.Animals;
using AutoMapper;
using Core.Enums.Animals;
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
				.ForMember(dest => dest.TemperFeatures, mOpt => mOpt.MapFrom(
					m => m.TemperFeatures.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(tf => (Temper)int.Parse(tf))))
				.ForMember(dest => dest.HealthConditions, mOpt => mOpt.MapFrom(
					m => m.HealthConditions.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(tf => (HealthCondition)int.Parse(tf))))
				.ForMember(dest => dest.ImagesSrc, mOpt => mOpt.MapFrom(m => m.ImagesNames.Split("|", StringSplitOptions.RemoveEmptyEntries)));

			CreateMap<CreateAnimalRequest, Animal>()
				.ForMember(dest => dest.MainImageName, mOpt => mOpt.MapFrom(m => m.Images[0].FileName))
				.ForMember(dest => dest.TemperFeatures, mOpt => mOpt.MapFrom(m => string.Join("|", m.TemperFeatures.Select(e => ((int)e).ToString()))))
				.ForMember(dest => dest.HealthConditions, mOpt => mOpt.MapFrom(m => string.Join("|", m.HealthConditions.Select(e => ((int)e).ToString()))))
				.ForMember(dest => dest.ImagesNames, mOpt => mOpt.MapFrom(m => string.Join("|", m.Images.Skip(1).Select(i => i.FileName))));
		}
	}
}
