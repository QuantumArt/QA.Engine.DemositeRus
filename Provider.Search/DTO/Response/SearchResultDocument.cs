using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Response;
public class SearchResultDocument
{
    [JsonPropertyName("_id")]
    public string? Id { get; }

    [JsonPropertyName("_index")]
    public string? Index { get; }

    [JsonPropertyName("_score")]
    public decimal Score { get; }

    [JsonPropertyName("SearchUrl")]
    public string? SearchUrl { get; }

    [JsonPropertyName("title")]
    public string? Title { get; }

    [JsonPropertyName("description")]
    public string? Description { get; }

    [JsonPropertyName("category")]
    public int? Category { get; }

    [JsonConstructor]
    public SearchResultDocument(
        string? id,
        string? index,
        decimal score,
        string? searchUrl,
        string? title,
        string? description,
        int? category)
    {
        Id = id;
        Index = index;
        Score = score;
        SearchUrl = searchUrl;
        Title = title;
        Description = description;
        Category = category;
    }
}
