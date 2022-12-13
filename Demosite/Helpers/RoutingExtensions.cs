using Microsoft.AspNetCore.Http;
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
        return request.Query.TryGetValue(Constants.BindNames.Pagination, out Microsoft.Extensions.Primitives.StringValues value)
            && int.TryParse(value.ToString(), out int intValue)
            ? Math.Max(1, intValue)
            : 1;
    }
}


