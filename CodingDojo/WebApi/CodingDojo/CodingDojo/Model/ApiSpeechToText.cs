using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodingDojo.Model
{
    public class ApiSpeechToText
    {
        public class FileUpload
        {
            [NotMapped]
            [JsonIgnore]
            public IFormFile Arquivo { get; set; }
        }
    }
}
