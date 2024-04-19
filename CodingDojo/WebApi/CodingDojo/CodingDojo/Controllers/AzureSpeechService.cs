using CodingDojo.Model;
using CodingDojo.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;


namespace CodingDojo.Controllers
{

    //Chaves: 879665279f8d459b88afac9d3bed7a63
    //endpoint: https://eastus.api.cognitive.microsoft.com/

    [Route("api/[controller]")]
    [ApiController]
    public class AzureSpeechService : ControllerBase
    {

        [HttpPost("TextoEmAudio")]
        public async Task<IActionResult> Post(Texto text)
        {
            try
            {
                

                var speechConfig = SpeechConfig.FromSubscription("e0b3832210e74f8e837fd10a4de7bcd0", "brazilsouth");

                using var speechRecognizer = new SpeechSynthesizer(speechConfig);

                var retorno = await speechRecognizer.SpeakTextAsync(text.text);

                var audio = retorno.AudioData;

                // Criar um arquivo temporário para armazenar os dados de áudio
                var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".wav");

                await System.IO.File.WriteAllBytesAsync(tempFilePath, audio);

                // Criar um objeto FormFile a partir do arquivo temporário
                using (var fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
                {
                    var formFile = new FormFile(fileStream, 0, audio.Length, "file", Path.GetFileName(tempFilePath))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "audio/wav" // Definir o tipo de conteúdo do arquivo de áudio
                    };

                    // Upload do arquivo de áudio
                    var teste = await AzureBlobStorageHelper.UploadAudio(formFile);

                    // Retornar o arquivo de áudio
                    return Ok(teste);
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);

            }

        }

        //[HttpPost]

        //public async Task<IActionResult> AudioEmTexto([FromForm] IFormFile audioFile)
        //{
        //    try
        //    {
        //        // Salvar o arquivo temporariamente no servidor
        //        var tempFilePath = Path.GetTempFileName();
        //        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        //        {
        //            await audioFile.CopyToAsync(stream);
        //        }

        //        // Configurar o serviço de reconhecimento de fala da Azure
        //        var speechConfig = SpeechConfig.FromSubscription("e0b3832210e74f8e837fd10a4de7bcd0", "brazilsouth");

        //        // Configurar o formato de áudio do arquivo
        //        var audioConfig = AudioConfig.FromWavFileInput(tempFilePath);

        //        // Inicializar o reconhecedor de fala
        //        using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
        //        {
        //            // Realizar o reconhecimento de fala
        //            var result = await recognizer.RecognizeOnceAsync();

        //            // Retornar o texto reconhecido
        //            return Ok(result.Text);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult?> FromFile([FromForm]SpeechConfig speechConfig)
        //{
        //    var speechConfigu = SpeechConfig.FromSubscription("e0b3832210e74f8e837fd10a4de7bcd0", "brazilsouth");
        //    using var audioConfig = AudioConfig.FromWavFileInput("PathToFile.wav");
        //        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        //        var result = await speechRecognizer.RecognizeOnceAsync();
        //        return Ok($"RECOGNIZED: Text={result.Text}");
          
        //}


        //[HttpPost("/SpeechToText")]
        //public async Task<IActionResult> PostAudio([FromForm] IFormFile file)
        //{
        //    try
        //    {
        //        // Verifique se o arquivo foi enviado
        //        if (file != null)
        //        {
        //            // Processar o arquivo de áudio, e salvar no diretório informado
        //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/audios");

        //            // Verifica se o diretório de destino especificado existe.
        //            if (!Directory.Exists(filePath))
        //            {
        //                // Cria o diretório se não existir
        //                Directory.CreateDirectory(filePath);
        //            }

        //            // Gera um nome de arquivo único
        //            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Arquivo.FileName);

        //            // Caminho completo do arquivo
        //            var fullPath = Path.Combine(filePath, fileName);

        //            // Salva o arquivo
        //            using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.Arquivo.CopyTo(fileStream);
        //            }

        //            // Validar o tipo do arquivo para confirmar o uso do áudio
        //            if (Path.GetExtension(file.Arquivo.FileName) != ".wav")
        //            {
        //                fullPath = await AzureService.Conversor(fullPath, fullPath.Replace(Path.GetExtension(file.Arquivo.FileName), ".wav"));
        //            }

        //            // Enviando para o serviço de tradução da fala
        //            var teste = AzureService.SpeechToText(fullPath);

        //            return Ok(new { Texto = teste.Result });
        //        }
        //        else
        //        {
        //            return BadRequest("Nenhum arquivo de áudio foi enviado.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Captura outras exceções
        //        return BadRequest(ex.Message);
        //    }
        //}




        //[HttpPost]
        //public async Task<IActionResult> AudioEmTexto([FromForm] IFormFile audioFile)
        //{
        //    try
        //    {
        //        if (audioFile == null || audioFile.Length == 0)
        //        {
        //            return BadRequest("Arquivo de áudio inválido.");
        //        }

        //        // Salvar o arquivo temporariamente no servidor
        //        var tempFilePath = Path.GetTempFileName();
        //        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        //        {
        //            await audioFile.CopyToAsync(stream);
        //        }

        //        // Configurar o serviço de reconhecimento de fala da Azure
        //        var speechConfig = SpeechConfig.FromSubscription("e0b3832210e74f8e837fd10a4de7bcd0", "brazilsouth");

        //        // Configurar o formato de áudio do arquivo
        //        var audioConfig = AudioConfig.FromWavFileInput(tempFilePath);

        //        // Inicializar o reconhecedor de fala
        //        using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
        //        {
        //            // Realizar o reconhecimento de fala
        //            var result = await recognizer.RecognizeOnceAsync();

        //            if (result.Reason == ResultReason.RecognizedSpeech)
        //            {
        //                // Retornar o texto reconhecido
        //                return Ok(result.Text);
        //            }
        //            else
        //            {
        //                return BadRequest("Não foi possível reconhecer o áudio.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log do erro
        //        return StatusCode(500, "Ocorreu um erro ao processar o áudio." + ex.Message);
        //    }


        //}
    }
}
