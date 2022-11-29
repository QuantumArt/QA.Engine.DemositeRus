using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request;

public class CompletionRequest
{
	[JsonPropertyName("$from")]
	public string From { get; } = "*";

	[JsonPropertyName("$query")]
	public string Query { get; }

	[JsonPropertyName("$where")]
	public RolesFilter? Filter { get; }

	public CompletionRequest(string query, string[] roles)
	{
		Query = query;
		Filter = new(roles);
	}
}
