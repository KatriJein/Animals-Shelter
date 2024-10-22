using Core.Enums.Animals;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Animals
{
	public record CreateAnimalRequest(int Age, Sex Sex, Size Size, Wool Wool, List<HealthCondition> HealthConditions, LivingCondition LivingCondition,
		ReceiptDate ReceiptDate, List<Temper> TemperFeatures, Color Color, string Name, string Breed, string Description, List<IFormFile> Images);
}
