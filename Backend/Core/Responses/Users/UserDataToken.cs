using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses.Users
{
	public class UserDataToken
	{
		public Guid Id { get; set; }
		public bool IsAdmin { get; set; }
	}
}
