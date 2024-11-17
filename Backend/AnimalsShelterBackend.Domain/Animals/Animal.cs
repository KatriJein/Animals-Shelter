using AnimalsShelterBackend.Domain.ShelterUser;
using Core.Enums.Animals;

namespace AnimalsShelterBackend.Domain.Animals
{
	public class Animal
	{
		public Guid Id { get; set; }
		public int Age { get; set; }
		public Sex Sex { get; set; }
		public Size Size { get; set; }
		public Wool Wool { get; set; }
		public Color Color { get; set; }
		public string HealthConditions { get; set; }
		public LivingCondition LivingCondition { get; set; }
		public ReceiptDate ReceiptDate { get; set; }
		public string TemperFeatures { get; set; }
		public string Name { get; set; }
		public string Breed { get; set; }
		public string MainImageSource { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public string ImagesSources { get; set; }

		public List<User> FavouritedByUsers { get; set; } = new List<User>();
	}
}
