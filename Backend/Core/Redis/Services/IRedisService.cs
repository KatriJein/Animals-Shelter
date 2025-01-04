using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis.Services
{
	public interface IRedisService
	{
		Task<T?> GetAsync<T>(string key);
		Task SetAsync<T>(string key, T value, TimeSpan expiresIn);
	}
}
