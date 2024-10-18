using Core.Enums.Animals;

namespace AnimalsShelterBackend.API.Responses.Animals
{
	public record AnimalResponse(Guid Id, int Age, Sex Sex, Size Size, Wool Wool, Temper Temper, string Color, string Name,
		string Breed, string? HealthDescription, List<string> VaccinationsInfo, List<string> SterilizationsInfo,
		List<string> RequiredConditions, List<string> BehaviourFeatures, List<string> OwnerWishes);
}
