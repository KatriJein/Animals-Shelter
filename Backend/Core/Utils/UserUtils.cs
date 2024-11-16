using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class UserUtils
	{
		public static long ConvertPhoneInputToEight(string login)
		{
			login = login.Replace("+7", "8");
			var isCorrectPhone = long.TryParse(login, out long phone);
			if (!isCorrectPhone) return 1;
			return phone;
		}

		public static string ConvertPhoneToPlusSeven(long phone)
		{
			var phoneString = phone.ToString();
			if (phoneString.Length < 2) return string.Empty;
			return "+7" + phoneString[1..];
		}

		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public static bool ArePasswordsEqual(string input, string hashed)
		{
			return BCrypt.Net.BCrypt.Verify(input, hashed);
		}
	}
}
