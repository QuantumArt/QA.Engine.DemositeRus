using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
