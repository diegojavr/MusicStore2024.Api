using AutoMapper;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;
        private readonly IConcertRepository _concertRepository;
        private readonly ICustomerRepository _customerRepository;

        public SaleService(ISaleRepository repository,
            ILogger<SaleService> logger,
            IMapper mapper,
            IConcertRepository concertRepository,
            ICustomerRepository customerRepository
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _concertRepository = concertRepository;
            _customerRepository = customerRepository;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(string email, SaleDtoRequest request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                await _repository.CreateTransactionAsync();
                var entity = _mapper.Map<Sale>(request);

                var customer = await _customerRepository.FindByEmailAsync(email);
                if (customer == null)
                {
                    throw new InvalidOperationException($"La cuenta {email} no está registrado como cliente");

                }

                entity.CustomerId = customer.Id;
                var concert = await _concertRepository.FindByIdAsync(request.ConcertId);
                if (concert == null)
                {
                    throw new Exception($"El concierto con ID {request.ConcertId} no existe");
                }

                if (DateTime.Today > concert.DateEvent)
                {
                    throw new InvalidOperationException($"No se puede comprar tickets del concierto {concert.Title} después de la fecha del evento");
                }

                entity.Total = entity.Quantity * concert.UnitPrice;
                await _repository.AddAsync(entity);
                await _repository.UpdateAsync(entity);

                response.Data = entity.Id;
                _logger.LogInformation("Se creo correctamente la venta para {email}", email);
                response.Success = true;
            }
            catch (Exception ex)
            {
                //Solo cuando hay alguna excepción se hace RollBack
                await _repository.RollBackAsync();
                _logger.LogError(ex, "Error agregando la venta {Message}", ex.Message);
                
            }

            return response;
        }

        public async Task<BaseResponseGeneric<SaleDtoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<SaleDtoResponse>();

            try
            {
                var sale = await _repository.FindByIdAsync(id);
                response.Data = _mapper.Map<SaleDtoResponse>(sale);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la venta";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<ReportDtoResponse>>> GetReportSaleAsync(DateTime dateStart, DateTime dateEnd)
        {
            var response = new BaseResponseGeneric<ICollection<ReportDtoResponse>>();
            try
            {
                var list = await _repository.GetReportSaleAsync(dateStart, dateEnd);
                response.Data = _mapper.Map<ICollection<ReportDtoResponse>>(list);
                response.Success = true;    
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al obtener los datos del reporte";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponsePagination<SaleDtoResponse>> ListAsync(DateTime dateStart, DateTime dateEnd, int page, int rows)
        {
            var response = new BaseResponsePagination<SaleDtoResponse>();
            try
            {
                var end = dateEnd.AddHours(23);

                //Tupla para repositorio ListAsync, predicado con el rango de fechas del SaleDate,
                //selector utilizando las propiedades del mapeo SaleDtoResponse y Sale,
                //orderBy por operationNumber, ventas de todos los usuarios,
                //pagina y filas
                var tupla = await _repository.ListAsync(p => p.SaleDate >= dateStart && p.SaleDate <= end,
                    p => _mapper.Map<SaleDtoResponse>(p),
                    x => x.OperationNumber,
                    page, rows);

                response.Data = tupla.Collection;
                response.TotalPages = Utilities.GetTotalPages(tupla.Total, rows);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las ventas por rango";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }
            return response;
        }

        public async Task<BaseResponsePagination<SaleDtoResponse>> ListAsync(string email, string? filter, int page, int rows)
        {
            var response = new BaseResponsePagination<SaleDtoResponse>();
            try
            {
                var tupla = await _repository.ListAsync(p=>p.Customer.Email.Equals(email)
                && p.Concert.Title.Contains(filter ?? string.Empty),
                p => _mapper.Map<SaleDtoResponse>(p),
                    x => x.SaleDate,
                    page, rows);
                response.Data = tupla.Collection;
                response.TotalPages=Utilities.GetTotalPages(tupla.Total,rows);
                response.Success = true;    

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las ventas por usuario";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }
            return response;
        }
    }
}
