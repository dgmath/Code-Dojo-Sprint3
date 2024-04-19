using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodingDojo.Interfaces
{
    public interface IAzureSpeechService
    {
        Task TextPVoice(string text);
        Task VoicePText(IFormFile voice, string text);
    }
}
