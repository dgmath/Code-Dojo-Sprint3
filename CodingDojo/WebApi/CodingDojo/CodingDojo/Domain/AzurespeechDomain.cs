using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodingDojo.Domain
{
    public class AzurespeechDomain
    {
        public string? Text { get; set; }

        [JsonIgnore]
        [NotMapped]
        public IFormFile? arquivo { get; set; }

        
    }
}
