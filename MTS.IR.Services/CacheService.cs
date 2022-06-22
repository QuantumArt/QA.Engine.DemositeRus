using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using MTS.IR.Interfaces;

namespace MTS.IR.Services
{
    public class CacheService: ICacheService
    {
        private IMemoryCache _memoryCache { get; }
        private int _cacheDuration { get; }
        public CacheService(IMemoryCache memoryCache,
                            IConfiguration config)
        {
            this._memoryCache = memoryCache;
            this._cacheDuration = int.Parse(config["Cache:Duration"]);
        }

        public T GetFromCache<T>(string key, Func<T> query)
        {
            T result = default(T);
            if (!_memoryCache.TryGetValue(key, out result))
            {
                result = query();
                if (result != null)
                {
                    _memoryCache.Set(key, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheDuration)));
                }
            }
            return result;
        }
    }
}
