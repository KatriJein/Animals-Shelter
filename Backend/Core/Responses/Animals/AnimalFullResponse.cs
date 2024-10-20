using Core.Enums.Animals;

namespace Core.Responses.Animals
{
	public class AnimalFullResponse
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
		public string MainImageSrc { get; set; }
		public string HealthDescription { get; set; }
		public List<string> VaccinationsInfo { get; set; }
		public List<string> SterilizationsInfo { get; set; }
		public List<string> RequiredConditions { get; set; }
		public List<string> BehaviourFeatures { get; set; }
		public List<string> OwnerWishes { get; set; }
		public List<string> ImagesSrc { get; set; }
	}
}
