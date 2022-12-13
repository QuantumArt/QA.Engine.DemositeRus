using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request
{
    public class CorrectionLimit
    {
        [JsonPropertyName("$ifFoundLte")]
        public int FoundLte { get; }

        public CorrectionLimit(int foundLte)
        {
            FoundLte = foundLte;
        }
    }
}
