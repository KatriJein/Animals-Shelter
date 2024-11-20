using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Constants
{
	public static class Const
	{
		#region animal
		public static string AnimalsBucketName = "animalspictures";
		#endregion

		#region articles
		public static string NewsArticlesBucketName = "articlesandnews";
		#endregion

		#region
		public static string UsersBucketName = "users";
		#endregion

		#region miscellaneous
		public static string FrontendCORS = "FrontendCORS";
		public static char Separator = '|';
		public static char UrlSeparator = '/';
		public static List<string> ArticlesAllowableContentTypes = new() { ".jpeg", ".png", ".jpg", };
		#endregion

		#region configSections
		public static string FrontendLink = "FrontendLink";
		public static string MinioLink = "MinioLink";
		public static string RabbitMQ = "RabbitMQ";
		public static string JWT = "JWT";
		#endregion

		#region queues
		#endregion

		#region regexes
		public static Regex GetUrlsFromArticleRegex = new Regex(@"<(img|video|audio).+?src=\\{0,}""([^\""]+?)\\{0,}"".*?>");
		public static Regex PhoneRegex = new Regex(@"^(\+7|8)\d{10}$");
		#endregion

	}
}
