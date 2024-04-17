using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Domain.Info;
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
        public async Task<BaseResponsePagination<ConcertDtoResponse>> ListAsync(string? filter, int page, int rows)
        {
            var response = new BaseResponsePagination<ConcertDtoResponse>();
            try
            {
                var tupla = await _repository
                    .ListAsync(predicate: p => p.Status && p.Title.Contains(filter ?? string.Empty),
                    selector: p => _mapper.Map<ConcertInfo>(p),
                    orderBy: p => p.Title, page, rows);

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
    }
}
