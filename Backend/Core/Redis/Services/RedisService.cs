using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Redis.Services
{
	internal class RedisService : IRedisService
	{
		private readonly IDistributedCache _cache;
		private readonly ILogger _logger;

		public RedisService(IDistributedCache cache, ILogger logger)
		{
			_cache = cache;
			_logger = logger;
		}

		public async Task<T?> GetAsync<T>(string key)
		{
			try
			{
				var cached = await _cache.GetStringAsync(key);
				if (cached == null) return default;
				var value = JsonSerializer.Deserialize<T>(cached);
				return value;
			}
			catch (Exception e)
			{
				_logger.Error($"Сбой в работе Redis! Не удалось получить данные. {e.Message}");
				return default;
			}
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan expiresIn)
		{
			var serialized = JsonSerializer.Serialize(value);
			var cachedOptions = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expiresIn };
			try
			{
				await _cache.SetStringAsync(key, serialized, cachedOptions);
			}
			catch (Exception e)
			{
				_logger.Error($"Сбой в работе Redis! Не удалось сохранить данные. {e.Message}");
			}
		}
	}
}
