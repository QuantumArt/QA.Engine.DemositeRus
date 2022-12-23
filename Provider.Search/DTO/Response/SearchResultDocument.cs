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
    public string? Category { get; }

    public SearchResultDocument(
        string? _id,
        string? _index,
        decimal _score,
        string? searchUrl,
        string? title,
        string? description,
        string? category)
    {
        Id = _id;
        Index = _index;
        Score = _score;
        SearchUrl = searchUrl;
        Title = title;
        Description = description;
        Category = category;
    }
}
