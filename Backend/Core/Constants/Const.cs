﻿using Core.Enums.Notifications;
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
		public static string TokenHeader = "Token";
		public static string CountHeader = "Total-Count";
		public static char Separator = '|';
		public static char UrlSeparator = '/';
		public static List<string> ArticlesAllowableContentTypes = new() { ".jpeg", ".png", ".jpg", };
		public static Dictionary<NotificationType, string> NotificationLinkPatterns = new()
		{
			{NotificationType.NewAnimal, "http://localhost:3000/animal/{0}"}
		};
		#endregion

		#region configSections
		public static string FrontendLinkMain = "FrontendLinkMain";
		public static string FrontendLinkAdditional = "FrontendLinkAdditional";
		public static string MinioLink = "MinioLink";
		public static string RedisLink = "RedisLink";
		public static string RabbitMQ = "RabbitMQ";
		public static string JWT = "JWT";
		#endregion

		#region queues
		public static string NotificationsQueue = "NotificationsQueue";
		#endregion

		#region regexes
		public static Regex GetUrlsFromArticleRegex = new Regex(@"<(img|video|audio).+?src=\\{0,}""([^\""]+?)\\{0,}"".*?>");
		public static Regex PhoneRegex = new Regex(@"^(\+7|8)\d{10}$");
		public static Regex EmailRegex = new Regex(@"^\S+@\S+\.\S+$");
		#endregion

		#region redisQueries
		public static string MostPopularArticlesKey = "PopularArticles";
		#endregion

	}
}
