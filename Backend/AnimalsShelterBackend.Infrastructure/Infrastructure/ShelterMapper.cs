using AnimalsShelterBackend.Domain.Animals;
using AutoMapper;
using Core.Constants;
using Core.Enums.Animals;
using Core.Requests.Animals;
using Core.Responses.Animals;
using Core.Utils;
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
				.ForMember(dest => dest.MainImageSrc, mOpt => mOpt.MapFrom(m => m.MainImageSource))
				.ForMember(dest => dest.TemperFeatures, mOpt => mOpt.MapFrom(
					m => m.TemperFeatures.Split(Const.Separator, StringSplitOptions.RemoveEmptyEntries).Select(tf => (Temper)int.Parse(tf))))
				.ForMember(dest => dest.HealthConditions, mOpt => mOpt.MapFrom(
					m => m.HealthConditions.Split(Const.Separator, StringSplitOptions.RemoveEmptyEntries).Select(tf => (HealthCondition)int.Parse(tf))))
				.ForMember(dest => dest.ImagesSrc, mOpt => mOpt.MapFrom(m => m.ImagesSources.Split(Const.Separator, StringSplitOptions.RemoveEmptyEntries)));

			CreateMap<Animal, AnimalShortResponse>()
				.ForMember(dest => dest.MainImageSrc, mOpt => mOpt.MapFrom(m => m.MainImageSource));

			CreateMap<CreateAnimalRequest, Animal>()
				.ForMember(dest => dest.TemperFeatures, mOpt => mOpt.MapFrom(m => string.Join(Const.Separator, m.TemperFeatures.Select(e => ((int)e).ToString()))))
				.ForMember(dest => dest.HealthConditions, mOpt => mOpt.MapFrom(m => string.Join(Const.Separator, m.HealthConditions.Select(e => ((int)e).ToString()))));
		}
	}
}
