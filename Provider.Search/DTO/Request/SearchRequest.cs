using System.Text.Json;
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

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("$where")]
    public RolesFilter? Filter { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("$correct")]
    public Correction? Correct { get; }

    public SearchRequest(string query, string[]? roles, int limit = 20, int offset = 0, int? ifFoundLte = null)
    {
        Query = query;
        Limit = limit;
        Offset = offset;
        if (roles != null) Filter = new(roles);
        if (ifFoundLte.HasValue) Correct = new(ifFoundLte.Value);
    }
}
