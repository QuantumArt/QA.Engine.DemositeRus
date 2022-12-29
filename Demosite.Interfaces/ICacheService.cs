using System;

namespace Demosite.Interfaces
{
    public interface ICacheService
    {
        T GetFromCache<T>(string key, string[] cacheTags, Func<T> query);
    }
}
