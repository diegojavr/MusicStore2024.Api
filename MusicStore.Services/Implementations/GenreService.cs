using Microsoft.Extensions.Logging;
using MusicStore.Domain;
using MusicStore.Dto.Request;
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
        public async Task<BaseResponseGeneric<int>> AddAsync(GenreDtoRequest request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var registro = new Genre
                {
                    Name = request.Name,
                    Status = request.Status,
                };

                var data = await _repository.AddAsync(registro);
                response.Success= true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al agregar";
                _logger.LogCritical(ex,"{ErrorMessage} {Message}", response.ErrorMessage,ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
               
                await _repository.DeleteAsync(id);
                response.Success= true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message); ;
            }
            return response;
        }

        public async Task<BaseResponseGeneric<GenreDtoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<GenreDtoResponse>();
            try
            {
                var registro = await _repository.FindByIdAsync(id);
                if ( registro == null)
                {
                    response.ErrorMessage = "No se encontró el registro";
                    response.Success= false;
                    return response;
                }

                response.Data = new GenreDtoResponse
                {
                    Id = registro.Id,
                    Name = registro.Name,
                    Status = registro.Status
                };

                response.Success= true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener";
                _logger.LogCritical(ex,"{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }
            return response;
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

        public async Task<BaseResponse> UpdateAsync(int id, GenreDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var registro = await _repository.FindByIdAsync(id);
                if ( registro == null)
                {
                    response.ErrorMessage = "No se encontró el registro";
                    response.Success= false;
                    return response;
                }
                registro.Name = request.Name;
                registro.Status = request.Status;

                await _repository.UpdateAsync( registro );
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message); ;
            }
            return response;
        }
    }
}
