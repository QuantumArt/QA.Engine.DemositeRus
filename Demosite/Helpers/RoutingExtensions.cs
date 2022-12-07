using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demosite.Helpers;

public static class Constants
{

    public static class BindNames
    {
        public const string Pagination = "page";
        public const string Item = "item";
    }
}

public static class RoutingExtensions
{
	public static int CurrentPaginationPageNumber(this HttpRequest request)
	{
		return request.Query.TryGetValue(Constants.BindNames.Pagination, out var value)
			   && int.TryParse(value.ToString(), out var intValue)
			? Math.Max(1, intValue)
			: 1;
	}
}


