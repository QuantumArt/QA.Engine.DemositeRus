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

	public SearchResponse(int status, int totalCount, SearchResultDocument[]? documents)
	{
		Status = status;
		TotalCount = totalCount;
		Documents = documents;
	}
}
