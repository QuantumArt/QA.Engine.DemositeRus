using Demosite.Helpers;
using Demosite.Interfaces;
using Demosite.ViewModels;
using emosite.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using Provider.Search.DTO.Response;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Controllers;
[Route("[controller]/[action]")]
public class SearchResultPageController : ContentControllerBase<SearchResultPage>
{
    private readonly ISearchService _searchService;
    private readonly ISiteSettingsService _siteSettingsProvider;

    public SearchResultPageController(ISearchService searchService,
                                ISiteSettingsService siteSettingsProvider)
    {
        _searchService = searchService;
        _siteSettingsProvider = siteSettingsProvider;

    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Search(string query, bool withCorrect, CancellationToken token)
    {
        int pageNumber = Request.CurrentPaginationPageNumber();
        pageNumber--; // start from zero page
        int itemsPerPage = await _siteSettingsProvider.GetSearchPaginatedItemsCountAsync(token);
        int? ifFoundLte = null;
        if (withCorrect)
        {
            ifFoundLte = await _siteSettingsProvider.GetSearchFoundLteAsync(token);
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Can't find items per page count", nameof(itemsPerPage));
        }

        SearchResponse response = await _searchService.SearchAsync(
            query,
            itemsPerPage,
            pageNumber * itemsPerPage,
            ifFoundLte,
            token);
        SearchResult result = new(response, itemsPerPage, query);

        return View("Index", result);
    }

}
