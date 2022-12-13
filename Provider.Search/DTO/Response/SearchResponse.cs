using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Response;

public class SearchResponse
{
    [JsonPropertyName("status")]
    public int Status { get; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; }

    [JsonPropertyName("documents")]
    public SearchResultDocument[]? Documents { get; }

    [JsonPropertyName("queryCorrection")]
    public QueryCorrection QueryCorrection { get; }

    public SearchResponse(int status, int totalCount, SearchResultDocument[]? documents, QueryCorrection correction)
    {
        Status = status;
        TotalCount = totalCount;
        Documents = documents;
        QueryCorrection = correction;
    }
}
