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
		public Temper Temper { get; set; }
		public string Color { get; set; }
		public string Name { get; set; }
		public string Breed { get; set; }
		public string MainImageName { get; set; }
		public string? HealthDescription { get; set; }
		public string? VaccinationsInfo { get; set; }
		public string? SterilizationsInfo { get; set; }
		public string? RequiredConditions { get; set; }
		public string? BehaviourFeatures { get; set; }
		public string? OwnerWishes { get; set; }
		public string? ImagesNames { get; set; }
	}
}
