using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
