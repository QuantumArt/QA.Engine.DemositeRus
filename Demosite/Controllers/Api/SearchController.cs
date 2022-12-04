using Demosite.Interfaces;
using Demosite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Controllers.Api;

[ApiController]
[Route("[controller]/[action]")]
public class SearchController : Controller
{
	private readonly ISearchService _searchService;

	public SearchController(ISearchService searchProvider)
	{
		_searchService = searchProvider;
	}

	[Produces("application/json")]
	[HttpPost]
	[IgnoreAntiforgeryToken]
	public async Task<IActionResult> Complete([FromBody] SearchRequest request, CancellationToken token)
	{
		var result = await _searchService.CompleteAsync(request.Query, token);
		return Ok(result);
	}
}
