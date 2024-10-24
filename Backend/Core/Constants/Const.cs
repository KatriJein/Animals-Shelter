using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
	public static class Const
	{
		#region animal
		public static string AnimalsBucketName = "animalspictures";
		#endregion

		#region miscellaneous
		public static string FrontendCORS = "FrontendCORS";
		public static char Separator = '|';
		public static char UrlSeparator = '/';
		#endregion

		#region configSections
		public static string FrontendLink = "FrontendLink";
		public static string MinioLink = "MinioLink";
		#endregion
	}
}
