using Demosite.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;

namespace Demosite.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly int _cacheDuration;
        public CacheService(IMemoryCache memoryCache,
                            IConfiguration config)
        {
            _memoryCache = memoryCache;
            _cacheDuration = int.Parse(config["Cache:Duration"]);
        }

        public T GetFromCache<T>(string key, Func<T> query)
        {
            if (!_memoryCache.TryGetValue(key, out T result))
            {
                result = query();
                if (result != null)
                {
                    _ = _memoryCache.Set(key, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheDuration)));
                }
            }
            return result;
        }
    }
}
