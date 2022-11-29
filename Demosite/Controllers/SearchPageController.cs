using Demosite.Helpers;
using Demosite.Services.Search;
using Demosite.ViewModels;
using emosite.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using Provider.Search;
using Provider.Search.DTO.Response;
using QA.DotNetCore.Engine.Routing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Controllers;
public class SearchPageController : ContentControllerBase<SearchPage>
{
	private readonly ISearchService _searchService;
    private readonly SearchSettings _settings;

    public SearchPageController(ISearchService searchService,
                                SearchSettings settings)
	{
		_searchService = searchService;
        _settings = settings;

    }

	public async Task<IActionResult> Index([FromQuery] string query, CancellationToken token)
	{
		int pageNumber = Request.CurrentPaginationPageNumber();
		pageNumber--; // start from zero page
		int itemsPerPage = _settings.SearchPaginatedItemsCount;

		if (itemsPerPage <= 0)
		{
			throw new ArgumentException("Can't find items per page count", nameof(itemsPerPage));
		}

		SearchResponse response = await _searchService.SearchAsync(
			query,
			itemsPerPage,
			pageNumber * itemsPerPage,
			token);
		SearchResult result = new(response, itemsPerPage);

		return View(result);
	}
}
