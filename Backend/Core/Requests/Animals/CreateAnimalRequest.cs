using Core.Enums.Animals;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Animals
{
	public record CreateAnimalRequest(int Age, Sex Sex, Size Size, Wool Wool, Temper Temper, string Color, string Name,
		string Breed, string HealthDescription, List<string> VaccinationsInfo, List<string> SterilizationsInfo,
		List<string> RequiredConditions, List<string> BehaviourFeatures, List<string> OwnerWishes, List<IFormFile> Images);
}
