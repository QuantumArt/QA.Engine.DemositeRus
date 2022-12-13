using Demosite.Helpers;
using Demosite.Interfaces;
using Demosite.Models.Pages;
using Demosite.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provider.Search.DTO.Response;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Controllers;
[Route("[controller]/[action]")]
public class SearchResultPageController : ContentControllerBase<SearchResultPage>
{
    private readonly ISearchService _searchService;
    private readonly ISiteSettingsService _siteSettingsProvider;
    private readonly ILogger<SearchResultPageController> _logger;

    public SearchResultPageController(ISearchService searchService,
                                ISiteSettingsService siteSettingsProvider,
                                ILogger<SearchResultPageController> logger)
    {
        _searchService = searchService;
        _siteSettingsProvider = siteSettingsProvider;
        _logger = logger;

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
        foreach (var doc in result.Documents.Where(d => string.IsNullOrEmpty(d.SearchUrl)))
        {
            _logger.LogError($"For {doc.Title} from {doc.Category} parameter `SearchUrl` is Empty or Null");
        }
        return View("Index", result);
    }
}
