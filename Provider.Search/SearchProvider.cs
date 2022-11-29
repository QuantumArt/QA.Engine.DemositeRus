using Provider.Search.DTO.Request;
using Provider.Search.DTO.Response;

namespace Provider.Search;

public class SearchProvider : ISearchProvider
{
	private readonly SearchApiClient _searchClient;

	public SearchProvider(SearchApiClient httpClient)
	{
		_searchClient = httpClient;
	}

	public async Task<SearchResponse> SearchAsync(string request, string[] roles, int limit, int offset, CancellationToken token)
	{
		SearchRequest searchRequest = new(request, roles, limit, offset);

		return await _searchClient.SearchAsync(searchRequest, token);
	}

	public async Task<string[]> CompleteAsync(string request, string[] roles, CancellationToken token)
	{
		CompletionRequest comnpletion = new(request, roles);

		var result = await _searchClient.CompleteAsync(comnpletion, token);

		return result.Phrases ?? Array.Empty<string>();
	}
}
