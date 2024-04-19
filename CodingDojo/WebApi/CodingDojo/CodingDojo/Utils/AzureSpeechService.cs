using Azure.Storage.Blobs;
using CodingDojo.Domain;
using CodingDojo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;

namespace CodingDojo.Utils
{
    //Criar classe estatica, onde podemos acessar seus métodos estaticos sem precisar instanciar a classe
    public static class AzureBlobStorageHelper
    {
        public static async Task<string> UploadAudio(IFormFile file)
        {
            //Receba tres parametros, imagem, string de conexao e o nome do container
            //String é para acessar corretamente e o container pois podem ter mais de um container

            try
            {
                //Caso exista um arquivo a lógica será iniciada
                if (file == null)
                {
                    return "Não foi possivel encontrar um arquivo";
                }

                //estamos criando um novo nome para o arquivo para um id com a extensão do arquivo exemplo jpg
                var blobName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                //Criando uma variavel do blob storage e passando a string de conexão, abrir conexao 
                var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=blobvitalhubmatheusd;AccountKey=b+G4c/mWTwenjeQ72QGjQ+bS6LP3cwQQklZs3fuRh8lTaUwAbp+6Ic/ThE1JIx3eDJuuy8WLQbBG+AStJeUKww==;EndpointSuffix=core.windows.net");

                //Obter o nome do conteiner que o arquivo sera salvo ser salvo
                var blobContainerClient = blobServiceClient.GetBlobContainerClient("containervitalhubmatheusd");

                //Nome do blob/ nome do arquivo
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                //Abre a conexao com o arquivo
                using (var stream = file.OpenReadStream())
                {
                    //Aguardo, acesso o blob e envio o stream(arquivo) e um parametro
                    await blobClient.UploadAsync(stream, true);
                }

                //Pego a uri do blob e retorno como string para o metodo
                return blobClient.Uri.ToString();

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
