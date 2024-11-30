using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Domain.Articles;
using AnimalsShelterBackend.Domain.ArticleViews;
using AnimalsShelterBackend.Domain.Notifications;
using AnimalsShelterBackend.Domain.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Domain.ShelterUser
{
	public class User
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Email { get; set; }
		public long Phone { get; set; }
		public string? AvatarSrc { get; set; }
		public string PasswordHash { get; set; }
		public bool IsAdmin { get; set; }
		public List<Article> Articles { get; set; } = new List<Article>();
		public List<Animal> FavouriteAnimals { get; set; } = new List<Animal>();
		public List<View> ArticleViews { get; set; } = new List<View>();
		public List<Notification> Notifications { get; set; } = new List<Notification>();
		public RefreshToken? RefreshToken { get; set; }
	}
}
