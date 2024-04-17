using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Domain;
using MusicStore.Domain.Info;
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
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _repository;
        private readonly ILogger<ConcertService> _logger;
        private readonly IMapper _mapper;

        public ConcertService(IConcertRepository repository, ILogger<ConcertService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(ConcertDtoRequest request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var registro = _mapper.Map<Concert>(request);
                // TODO: Funcionalidad de subir concierto
                await _repository.AddAsync(registro);
                response.Data = registro.Id;
                response.Success = true;

                _logger.LogInformation("Concierto agregado con exito");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar al concierto";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
            
        }


        public async Task<BaseResponseGeneric<ConcertDtoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ConcertDtoResponse>();
            try
            {
                var concert = await _repository.FindByIdAsync(id);
                if (concert == null)
                {
                    response.ErrorMessage = "No se encontró el concierto";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ConcertDtoResponse>(concert);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener el concierto";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            
            }

            return response;
        }

        public async Task<BaseResponsePagination<ConcertDtoResponse>> ListAsync(string? filter, int page, int rows)
        {
            var response = new BaseResponsePagination<ConcertDtoResponse>();
            try
            {
                var tupla = await _repository
                    .ListAsync(filter, page, rows);

                var total = tupla.Total / rows;
                if (tupla.Total % rows > 0)
                {
                    total++;
                }
                response.Data = tupla.Collection
                    .Select(p => _mapper.Map<ConcertDtoResponse>(p))
                    .ToList();
                response.TotalPages = total;
                response.Success= true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los conciertos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ConcertDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var concert = await _repository.FindByIdAsync(id);
                if (concert == null)
                {
                    response.ErrorMessage = "No se encontró el concierto";
                    response.Success = false;
                    return response;
                }
                _mapper.Map(request,concert);
                if (!string.IsNullOrEmpty(request.FileName)) { 
                // TODO: Agregar funcionalidad de cambio de imagen
                }

                await _repository.UpdateAsync(concert);
                response.Success = true;

                _logger.LogInformation("Concierto actualizado con exito");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los conciertos";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
               
            }
            return response;
        }
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar el concierto";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }
    }
}
