using AnimalsShelterBackend.Domain.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.BusServices
{
	public interface INotificationsBusService
	{
		Task SendNewAnimalInfoAsync(Animal animalData, Guid animalId);
	}
}
