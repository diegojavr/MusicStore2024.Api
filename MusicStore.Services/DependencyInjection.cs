using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Domain;
using MusicStore.Repositories.Implementations;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Implementations;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IGenreService, GenreService>()
                .AddTransient<IConcertService, ConcertService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<ISaleService, SaleService>();
            
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IGenreRepository, GenreRepository>()
                .AddTransient<IConcertRepository, ConcertRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<ISaleRepository, SaleRepository>();
        }

        public static IServiceCollection AddUploader(this IServiceCollection services, IConfiguration configuration)
        {
            //Si consigue el valor core.windows.net utiliza el uploader a Azure, sino será local
            if (configuration.GetSection("StorageConfiguration:PublicUrl").Value!.Contains("core.windows.net"))
            {
                services.AddTransient<IFileUploader, AzureBlobStorageUploader>();
            }
            else
                services.AddTransient<IFileUploader, FileUploader>();

            return services;
        }
    }
}
