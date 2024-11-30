using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.Contributors;
using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Domain.ShelterUser;
using AutoMapper;
using Core.Constants;
using Core.Enums.Animals;
using Core.Requests.Animals;
using Core.Requests.Articles;
using Core.Requests.Contributors;
using Core.Requests.Notifications;
using Core.Requests.Users;
using Core.Responses.Animals;
using Core.Responses.Articles;
using Core.Responses.Contributors;
using Core.Responses.Notifications;
using Core.Responses.Users;
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
			MapUsers();
			MapContributors();
			MapArticles();
			MapNotifications();
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

		private void MapUsers()
		{
			CreateMap<User, UserResponse>()
				.ForMember(ur => ur.Phone, mOpt => mOpt.MapFrom(u => UserUtils.ConvertPhoneToPlusSeven(u.Phone)));
		}

		private void MapContributors()
		{
			CreateMap<Contributor, ContributorResponse>();
			CreateMap<CreateContributorRequest, Contributor>();
		}

		private void MapArticles()
		{
			CreateMap<Article, ArticleShortResponse>();
			CreateMap<Article, ArticleFullResponse>()
				.ForPath(afr => afr.Author.Id, opt => opt.MapFrom(a => a.UserId))
				.ForPath(afr => afr.Author.Name, opt => opt.MapFrom(a => a.User == null ? "DELETED" : a.User.Name))
				.ForPath(afr => afr.Author.Surname, opt => opt.MapFrom(a => a.User == null ? "" : a.User.Surname));
				

			CreateMap<CreateArticleRequest, Article>()
				.ForMember(a => a.CreatedAt, opt => opt.MapFrom(a => DateTime.UtcNow))
				.ForMember(a => a.LastUpdatedAt, opt => opt.MapFrom(a => DateTime.UtcNow));
		}

		private void MapNotifications()
		{
			CreateMap<Notification, NotificationResponse>();
			CreateMap<CreateNotificationRequest, Notification>();
		}
	}
}
