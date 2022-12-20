using Demosite.Interfaces;
using Demosite.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<string[]> Complete([FromBody] SearchRequest request, CancellationToken token)
    {
        return await _searchService.CompleteAsync(request.Query, token);
    }
}
