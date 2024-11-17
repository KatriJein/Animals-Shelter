using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Users
{
	public record UserLoginRequest(string Login, string Password);
}
