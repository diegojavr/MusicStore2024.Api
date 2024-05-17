using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Domain;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services.Interfaces;
using System.Security.Claims;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleService service, ILogger<SalesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolCustomer)]
        public async Task<IActionResult> Post(SaleDtoRequest request)
        {
            var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;
            var response = await _service.AddAsync(email, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListSalesByDate")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> GetListSalesByDate(string dateStart, string dateEnd, int page = 1, int rows = 10)
        {
            try
            {
                var response = await _service.ListAsync(DateTime.Parse(dateStart), DateTime.Parse(dateEnd), page, rows);
                return Ok(response);
            }
            catch (FormatException ex)
            {

                _logger.LogWarning(ex, "Error de conversión de formato de fecha {Message}", ex.Message);
                return BadRequest(new BaseResponse { ErrorMessage = "Error de conversion de fecha" });
            }
        }

        [HttpGet("ListSales")]
        [Authorize]
        public async Task<IActionResult> GetListSales(string? filter, int page = 1, int rows = 10)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            var response = await _service.ListAsync(email, filter, page, rows);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
