using Provider.Search.DTO.Response;
using System.Threading;
using System.Threading.Tasks;

namespace Demosite.Interfaces;

public interface ISearchService
{
    Task<SearchResponse> SearchAsync(string query, int limit, int offset, int? ifFoundLte, CancellationToken token);

    Task<string[]> CompleteAsync(string query, CancellationToken token);
}
