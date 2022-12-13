using Demosite.Helpers;
using Demosite.Interfaces;
using Demosite.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demosite.Components;

public class PaginationViewComponent : ViewComponent
{
    private readonly ISiteSettingsService _siteSettingsProvider;

    public PaginationViewComponent(ISiteSettingsService siteSettingsProvider)
    {
        _siteSettingsProvider = siteSettingsProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync(int pagesCount)
    {
        int currentPage = Request.CurrentPaginationPageNumber();
        int paginationViewCount =
            await _siteSettingsProvider.PaginationViewCountAsync(HttpContext.RequestAborted);

        (int minValue, int maxValue) = Interval(paginationViewCount, pagesCount, currentPage);
        PaginationViewModel model = new()
        {
            Current = currentPage,
            MinValue = minValue,
            MaxValue = maxValue,
            BaseQuery = GetBaseQueryString(HttpContext)
        };
        return View(model);
    }

    private static (int, int) Interval(int paginationViewCount, int total, int current)
    {
        int near = paginationViewCount / 2;

        int expectedMinValue = current - near;
        int expectedMaxValue = current + near;

        int minValue = Math.Max(expectedMinValue, 1);
        int minValueDelta = minValue - expectedMinValue;
        int maxValue = Math.Min(expectedMaxValue + minValueDelta, total);
        return (minValue, maxValue);
    }

    private static QueryString GetBaseQueryString(HttpContext context)
    {
        QueryBuilder builder = new();

        foreach ((string key, Microsoft.Extensions.Primitives.StringValues values) in context.Request.Query)
        {
            if (key.Equals(Constants.BindNames.Pagination, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            builder.Add(key, values.AsEnumerable());
        }

        return builder.ToQueryString();
    }
}
