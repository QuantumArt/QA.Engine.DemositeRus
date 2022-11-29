using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request;

public class SearchRequest
{
	[JsonPropertyName("$from")]
	public string From { get; } = "*";

	[JsonPropertyName("$query")]
	public string Query { get; }

	[JsonPropertyName("$limit")]
	public int Limit { get; }

	[JsonPropertyName("$offset")]
	public int Offset { get; }

	[JsonPropertyName("$where")]
	public RolesFilter? Filter { get; }

	public SearchRequest(string query, string[] roles, int limit = 20, int offset = 0)
	{
		Query = query;
		Limit = limit;
		Offset = offset;
		Filter = new(roles);
	}
}
