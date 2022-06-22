using QA.DotNetCore.Engine.Abstractions;
using System.Collections.Generic;

namespace Demosite.ViewModels.Helpers
{
    public static class AbstractPageExtension
    {
        public static List<BreadCrumbViewModel> GetBreadCrumbs(this IAbstractItem currentPage)
        {
            if (currentPage is IStartPage)
                return new List<BreadCrumbViewModel>();
            var breadCrumbs = currentPage.Parent.GetBreadCrumbs();
            breadCrumbs.Add(new BreadCrumbViewModel
            {
                Text = currentPage.Title,
                Url = FormatUrl(currentPage.GetUrl())
            });
            return breadCrumbs;
        }

        public static string FormatUrl(string url)
        {
            return url.LastIndexOf('\\') == url.Length - 1 ? url : $@"{url}\";
        }
    }


}
