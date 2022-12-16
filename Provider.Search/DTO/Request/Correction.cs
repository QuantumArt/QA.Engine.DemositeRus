using System.Text.Json.Serialization;

namespace Provider.Search.DTO.Request
{
    public class Correction
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("$results")]
        public CorrectionLimit Result { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("$query")]
        public CorrectionLimit Query { get; }
        public Correction(int ifFoundLte, bool withCorrect)
        {
            if (withCorrect) Result = new CorrectionLimit(ifFoundLte);
            if (!withCorrect) Query = new CorrectionLimit(ifFoundLte);
        }
    }
}
