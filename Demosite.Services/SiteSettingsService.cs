using Demosite.Interfaces;
using Demosite.Interfaces.Dto;
using Demosite.Postgre.DAL;
using Demosite.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CacheTagUtilities = Demosite.Templates.CacheTagUtilities;

namespace Demosite.Services;

public class SiteSettingsService : ISiteSettingsService
{
    private readonly IDbContext _context;
    public readonly ICacheService _memoryCache;
    private readonly CacheTagUtilities _cacheTagUtilities;
    private readonly QpExtSettings _qpSettings;

    public SiteSettingsService(
        IDbContext context,
        ICacheService cacheProvider,
        CacheTagUtilities cacheTagUtilities,
        IOptions<QpExtSettings> qpSettings)
    {
        _context = context;
        _memoryCache = cacheProvider;
        _cacheTagUtilities = cacheTagUtilities;
        _qpSettings = qpSettings.Value;
    }

    /// <summary>
    /// Количество страниц в пагинации
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> PaginationViewCountAsync(CancellationToken cancellationToken)
    {
        return GetSiteSettings("PaginationViewCount", default(int), cancellationToken);
    }

    /// <summary>
    /// Количество элементов в списках новостей на одной странице
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> NewsPaginatedItemsCountAsync(CancellationToken cancellationToken)
    {
        return GetSiteSettings("NewsPaginatedItemsCount", default(int), cancellationToken);
    }

    /// <summary>
    /// Количество элементов в результатах поиска на одной странице
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> GetSearchPaginatedItemsCountAsync(CancellationToken cancellationToken)
    {
        return GetSiteSettings("SearchPaginatedItemsCount", default(int), cancellationToken);
    }


    /// <summary>
    /// Если при поиске найдено меньше, поисковая система может предложить исправление запроса
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int?> GetSearchFoundLteAsync(CancellationToken cancellationToken)
    {
        return GetSiteSettings("SearchFoundLteCount", default(int?), cancellationToken);
    }

    /// <summary>
    /// Получить значение из настроек сайта
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private async Task<T> GetSiteSettings<T>(string name, T defaultValue,
        CancellationToken cancellationToken)
    {
        IDictionary<string, SiteSettingDto> siteSettings = await GetSiteSettings(cancellationToken);

        if (!siteSettings.TryGetValue(name, out SiteSettingDto? setting))
        {
            return defaultValue;
        }

        if (setting.Type == SettingType.Image && typeof(T) != typeof(string))
        {
            throw new InvalidOperationException(
                "Тип настройки \"изображение\" должен быть возвращен с типом string");
        }

        string value = setting.Type switch
        {
            SettingType.Image => setting.ImageValueUrl,
            _ => setting.StringValue
        };
        return MapSetting(value, defaultValue);
    }

    private Task<IDictionary<string, SiteSettingDto>> GetSiteSettings(
        CancellationToken cancellationToken)
    {
        const string settingsCacheKey =
            nameof(SiteSettingsService) + "." + nameof(GetSiteSettings);

        return _memoryCache.GetFromCache<Task<IDictionary<string, SiteSettingDto>>>(settingsCacheKey, async () =>
        {
            SiteSetting[] settings = await (_context as PostgreQpDataContext).SiteSettings
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);

            return settings.ToDictionary(x => x.Key, Map);
        });
    }

    private static T MapSetting<T>(string? settingValue, T defaultValue)
    {
        if (string.IsNullOrEmpty(settingValue))
        {
            return defaultValue;
        }

        object objResult = ConvertSettingValue(settingValue);

        return objResult is null
            ? defaultValue
            : (T)objResult;

        static object? ConvertSettingValue(string? settingValue)
        {
            Type type = typeof(T);
            if (type == typeof(string))
            {
                return Convert.ToString(settingValue);
            }

            return type == typeof(double) || type == typeof(double?)
                ? Convert.ToDouble(settingValue)
                : type == typeof(int) || type == typeof(int?)
                ? Convert.ToInt32(settingValue)
                : type == typeof(long) || type == typeof(long?)
                ? Convert.ToInt64(settingValue)
                : type == typeof(DateTime) || type == typeof(DateTime?)
                ? Convert.ToDateTime(settingValue)
                : type == typeof(bool) || type == typeof(bool?)
                ? Convert.ToBoolean(settingValue)
                : settingValue;
        }
    }

    public static SiteSettingDto Map(SiteSetting siteSetting)
    {
        return new(siteSetting.Key,
            siteSetting.TypeExact,
            siteSetting.StringValue,
            siteSetting.ImageValueUrl);
    }
}
