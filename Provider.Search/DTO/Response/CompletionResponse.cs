using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Response;
public class CompletionResponse
{
    [JsonPropertyName("status")]
    public int Status { get; }

    [JsonPropertyName("phrases")]
    public string[]? Phrases { get; }

    public CompletionResponse(int status, string[]? phrases)
    {
        Status = status;
        Phrases = phrases;
    }
}
