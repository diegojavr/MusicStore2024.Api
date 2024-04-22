using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MusicStore.Domain.Configuration;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    public class AzureBlobStorageUploader : IFileUploader
    {
        private readonly ILogger<FileUploader> _logger;
        private readonly AppConfig _appConfig;

        public AzureBlobStorageUploader(IOptions<AppConfig> options, ILogger<FileUploader> logger)
        {
            _logger = logger;
            _appConfig = options.Value;
        }
        public async Task<string> UploadFileAsync(string? base64Image, string? fileName)
        {
            if (string.IsNullOrWhiteSpace(base64Image) || string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            try
            {
                //variable con la conexion a la cuenta de almacenamiento de Azure
                var client = new BlobServiceClient(_appConfig.StorageConfiguration.Path);
                //nombre del contenedor 
                var container = client.GetBlobContainerClient("images");
                
                //blob tiene referencia del archivo
                 var blob= container.GetBlobClient(fileName);
                await using var stream = new MemoryStream(Convert.FromBase64String(base64Image));
                await blob.UploadAsync(stream,overwrite: true);

                _logger.LogInformation("Se subió correctamente el archivo a Azure");
                return $"{_appConfig.StorageConfiguration.PublicUrl}{fileName}";

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al subir el archivoa a Azure {fileName} {Message}", fileName, ex.Message);
                return string.Empty;
            }
        }
    }
}
