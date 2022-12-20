using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request;
public class CompletionRequest
{
    [JsonPropertyName("$from")]
    public string From { get; } = "*";

    [JsonPropertyName("$query")]
    public string Query { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("$where")]
    public RolesFilter? Filter { get; }

    public CompletionRequest(string query, string[] roles)
    {
        Query = query;
        if (roles != null) Filter = new(roles);
    }
}
