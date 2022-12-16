using Demosite.Interfaces;
using Provider.Search;
using Provider.Search.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Services.Search;

public class SearchService : ISearchService
{
    private readonly ISearchProvider _searchProvider;

    public SearchService(ISearchProvider searchPrivuder)
    {
        _searchProvider = searchPrivuder;
    }

    public async Task<string[]> CompleteAsync(string query, CancellationToken token)
    {
        return await _searchProvider.CompleteAsync(query, null, token);
    }

    public async Task<SearchResponse> SearchAsync(string query, int limit, int offset, int? ifFoundLte, bool withCorrect, CancellationToken token)
    {
        return await _searchProvider.SearchAsync(query, null, limit, offset, ifFoundLte, withCorrect, token);
    }
}
