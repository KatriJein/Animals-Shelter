using Core.Constants;
using Core.Responses.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class TokenUtils
	{
		public static string CreateToken(UserDataToken userDataToken, DateTime expirationDate, IConfiguration config)
		{
			var claims = new List<Claim>()
			{
				new("Id", userDataToken.Id.ToString()),
				new("IsAdmin", userDataToken.IsAdmin.ToString())
			};
			if (userDataToken.IsAdmin)
				claims.Add(new(ClaimTypes.Role, "Admin"));
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Const.JWT]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var securityToken = new JwtSecurityToken(claims: claims, expires: expirationDate, signingCredentials: credentials);
			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}
	}
}
