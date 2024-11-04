﻿using System;
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

		#region
		public static string NewsArticlesBucketName = "articlesandnews";
		#endregion

		#region miscellaneous
		public static string FrontendCORS = "FrontendCORS";
		public static char Separator = '|';
		public static char UrlSeparator = '/';
		public static List<string> ArticlesAllowableContentTypes = new() { ".mp4", ".jpeg", ".amv", ".png", ".jpg", ".avi" };
		#endregion

		#region configSections
		public static string FrontendLink = "FrontendLink";
		public static string MinioLink = "MinioLink";
		public static string RabbitMQ = "RabbitMQ";
		#endregion

		#region queues
		#endregion

		#region regexes
		public static Regex GetUrlsFromArticleRegex = new Regex(@"<(img|video|audio).+?src=\\{0,}""([^\""]+?)\\{0,}"".*?>");
		#endregion
	}
}
