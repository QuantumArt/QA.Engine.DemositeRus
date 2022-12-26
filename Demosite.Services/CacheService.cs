using Demosite.Interfaces;
using Demosite.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QA.DotNetCore.Caching.Interfaces;
using System;

namespace Demosite.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IOptionsMonitor<CacheSettings> _cacheSettings;
        public CacheService(ICacheProvider cacheProvider,
                            IOptionsMonitor<CacheSettings> cacheSettings)
        {
            _cacheProvider = cacheProvider;
            _cacheSettings = cacheSettings;
        }

        public T GetFromCache<T>(string key, string[] cacheTags, Func<T> query)
        {
            return _cacheProvider.GetOrAdd(
                key,
                cacheTags,
                _cacheSettings.CurrentValue.Duration,
                query);
        }
    }
}
