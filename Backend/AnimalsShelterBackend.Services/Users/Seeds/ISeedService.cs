using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Services.Users.Seeds
{
	public interface ISeedService
	{
		Task CreateAdminUser();
	}
}
