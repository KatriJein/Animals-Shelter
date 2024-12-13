using Core.Redis.Services;
using Microsoft.AspNetCore.Http;
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

		public static void AddHeaderToResponse<T>(HttpContext context, string header, T value)
		{
			context.Response.Headers[header] = value.ToString();
		}

		public static async Task<T> GetWithCachingAsync<T>(IRedisService redisService, string key, TimeSpan expiresIn, Func<Task<T>> function)
		{
			var entries = await redisService.GetAsync<T>(key);
			if (!EqualityComparer<T>.Default.Equals(entries, default)) return entries;
			var result = await function();
			if (result != null)
				await redisService.SetAsync(key, result, expiresIn);
			return result;
		}
	}
}
