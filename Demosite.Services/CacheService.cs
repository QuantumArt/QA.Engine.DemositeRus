using Demosite.Interfaces;
using Microsoft.Extensions.Configuration;
using QA.DotNetCore.Caching.Interfaces;
using System;

namespace Demosite.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly TimeSpan _cacheDuration;
        public CacheService(ICacheProvider cacheProvider,
                            IConfiguration config)
        {
            _cacheProvider = cacheProvider;
            _cacheDuration = TimeSpan.FromSeconds(int.Parse(config["Cache:Duration"]));
        }

        public T GetFromCache<T>(string key, string[] cacheTags, Func<T> query)
        {
            return _cacheProvider.GetOrAdd(
                key,
                cacheTags,
                _cacheDuration,
                query);
        }
    }
}
