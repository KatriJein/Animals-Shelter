using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class UserUtils
	{
		public static bool TryConvertPhoneInputToEight(string login, out long phone)
		{
			var phoneMatch = Const.PhoneRegex.Match(login);
			if (!phoneMatch.Success)
			{
				phone = 1;
				return false;
			}
			login = login.Replace("+7", "8");
			phone = long.Parse(login);
			return true;
		}

		public static bool CheckPhone(string login, long userPhone)
		{
			var converted = TryConvertPhoneInputToEight(login, out long phone);
			if (!converted) return false;
			return userPhone == phone;
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

		public static string CreateFeedbackName(string name, string surname)
		{
			return $"{name} {char.ToUpper(surname[0])}.";
		}
	}
}
