using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
	public static class CommonUtils
	{
		public static bool IsNullable<T>(List<T?>? en)
		{
			return en == null || en.Count == 1 && en[0] == null;
		}
	}
}
