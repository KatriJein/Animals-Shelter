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
		public Color Color { get; set; }
		public List<HealthCondition> HealthConditions { get; set; }
		public LivingCondition LivingCondition { get; set; }
		public ReceiptDate ReceiptDate { get; set; }
		public List<Temper> TemperFeatures { get; set; }
		public string Name { get; set; }
		public string Breed { get; set; }
		public string MainImageSrc { get; set; }
		public string Description { get; set; }
		public List<string> ImagesSrc { get; set; }
	}
}
