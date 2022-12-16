using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Response
{
    public class QueryCorrection
    {
        [JsonPropertyName("text")]
        public string Text { get; }

        [JsonPropertyName("snippet")]
        public string Snippet { get; }

        [JsonPropertyName("resultsAreCorrected")]
        public bool ResultsAreCorrected { get; }

        public QueryCorrection(string text, string snippet, bool resultsAreCorrected)
        {
            Text = text;
            Snippet = snippet;
            ResultsAreCorrected = resultsAreCorrected;
        }
    }
}
