using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Domain.Configuration;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    public class FileUploader : IFileUploader
    {
        private readonly ILogger<FileUploader> _logger;
        private readonly AppConfig _appConfig;

        public FileUploader(IOptions<AppConfig> options, ILogger<FileUploader> logger)
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
                var bytes = Convert.FromBase64String(base64Image); //convertimos imagen
                var path = Path.Combine(_appConfig.StorageConfiguration.Path,fileName);
                await using var fileStream = new FileStream(path, FileMode.Create);
                await fileStream.WriteAsync(bytes,0,bytes.Length); //arreglo de 'bytes', posicion 0 y toda la longitud

                return $"{_appConfig.StorageConfiguration.PublicUrl}{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al subir el archivo {fileName} {Message}", fileName, ex.Message);
                return string.Empty;
            }

        }
    }
}
