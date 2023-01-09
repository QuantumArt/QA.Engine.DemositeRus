using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Provider.Search.DTO.Request;
using Provider.Search.DTO.Response;
using System.Net.Http.Json;

namespace Provider.Search;
public class SearchApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<SearchApiClient> _logger;
    private readonly SearchSettings _settings;
    public SearchApiClient(
        HttpClient client,
        IOptions<SearchSettings> settings,
        ILogger<SearchApiClient> logger)
    {
        _settings = settings.Value;
        client.BaseAddress = new Uri(_settings.BaseUrl);
        _client = client;
        _logger = logger;
    }

    public Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken token)
    {
        return SendAsync<SearchRequest, SearchResponse>(request, _settings.SearchPath, token);
    }

    public Task<CompletionResponse> CompleteAsync(CompletionRequest request, CancellationToken token)
    {
        return SendAsync<CompletionRequest, CompletionResponse>(request, _settings.CompletionPath, token);
    }

    private async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, string path, CancellationToken token)
        where TRequest : class
        where TResponse : class
    {
        _logger.LogInformation(
            "Sending search request {SearchRequest} (base: {SearchBaseUrl}, uri: {SearchPath})",
            request,
            _settings.BaseUrl,
            path);

        var response = await _client.PostAsync(path, JsonContent.Create(request), token);
        response = response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);

        if (result is null)
        {
            _logger.LogError("Unable to deserialize response to type {Type}", typeof(TResponse));
            throw new InvalidOperationException("Can't deserialize response.");
        }

        _logger.LogInformation("Successfully obtained search results");
        return result;
    }
}
