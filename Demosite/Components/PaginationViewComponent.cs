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
	private readonly ISiteSettingsProvider _siteSettingsProvider;

	public PaginationViewComponent(ISiteSettingsProvider siteSettingsProvider)
	{
		_siteSettingsProvider = siteSettingsProvider;
	}

	public async Task<IViewComponentResult> InvokeAsync(int pagesCount)
	{
		var currentPage = Request.CurrentPaginationPageNumber();
		var paginationViewCount =
			await _siteSettingsProvider.PaginationViewCountAsync(HttpContext.RequestAborted);

		var (minValue, maxValue) = Interval(paginationViewCount, pagesCount, currentPage);
		var model = new PaginationViewModel
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
		var near = paginationViewCount / 2;

		var expectedMinValue = current - near;
		var expectedMaxValue = current + near;

		var minValue = Math.Max(expectedMinValue, 1);
		var minValueDelta = minValue - expectedMinValue;
		var maxValue = Math.Min(expectedMaxValue + minValueDelta, total);
		return (minValue, maxValue);
	}

	private static QueryString GetBaseQueryString(HttpContext context)
	{
		var builder = new QueryBuilder();

		foreach ((var key, var values) in context.Request.Query)
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
