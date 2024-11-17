using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests.Users
{
	public record UpdateUserRequest(string? Name, string? Surname, string? Email, string? Phone) : IUpdateRequest;
}
