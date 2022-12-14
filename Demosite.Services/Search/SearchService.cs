using Demosite.Interfaces;
using Provider.Search;
using Provider.Search.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Services.Search;

public class SearchService : ISearchService
{
    private readonly ISearchProvider _searchProvider;
    private const string DEFAULT_ROLE = "Reader";

    public SearchService(ISearchProvider searchPrivuder)
    {
        _searchProvider = searchPrivuder;
    }

    public async Task<string[]> CompleteAsync(string query, CancellationToken token)
    {
        string[] userRoles = GetUserRolesAsync(token);
        return await _searchProvider.CompleteAsync(query, userRoles, token);
    }

    public async Task<SearchResponse> SearchAsync(string query, int limit, int offset, int? ifFoundLte, bool withCorrect, CancellationToken token)
    {
        string[] userRoles = GetUserRolesAsync(token);
        return await _searchProvider.SearchAsync(query, userRoles, limit, offset, ifFoundLte, withCorrect, token);
    }

    private string[] GetUserRolesAsync(CancellationToken token)
    {
        return new string[1] { DEFAULT_ROLE };
    }
}
