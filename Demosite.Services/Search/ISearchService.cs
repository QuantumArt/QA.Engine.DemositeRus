using Provider.Search.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Services.Search;

public interface ISearchService
{
	Task<SearchResponse> SearchAsync(string query, int limit, int offset, CancellationToken token);

	Task<string[]> CompleteAsync(string query, CancellationToken token);
}
