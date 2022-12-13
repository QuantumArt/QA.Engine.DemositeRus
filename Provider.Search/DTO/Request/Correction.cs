using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request
{
    public class Correction
    {
        [JsonPropertyName("$results")]
        public CorrectionLimit Result { get; }

        public Correction(int ifFoundLte)
        {
            Result = new CorrectionLimit(ifFoundLte);
        }
    }
}
