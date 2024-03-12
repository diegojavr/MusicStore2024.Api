using Microsoft.Extensions.Logging;
using MusicStore.Dto.Response;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly ILogger<GenreService> _logger;//Bitacora

        public GenreService(IGenreRepository repository,ILogger<GenreService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public Task<BaseResponseGeneric<int>> AddAsync(GenreDtoResponse request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseGeneric<GenreDtoResponse>> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseGeneric<ICollection<GenreDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<GenreDtoResponse>>();
            try
            {
                var data = await _repository.ListAsync();

                response.Data = data.Select(p => new GenreDtoResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                }).ToList();

                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message); ;

                
            }
            return response;
        }

        public Task<BaseResponse> UpdateAsync(int id, GenreDtoResponse request)
        {
            throw new NotImplementedException();
        }
    }
}
