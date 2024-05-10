using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Domain;
using MusicStore.Dto.Request;
using MusicStore.Services.Interfaces;
using System.Security.Claims;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;

        public SalesController(ISaleService service)
        {
            _service=service;
        }

        [HttpPost]
        [Authorize(Roles =Constantes.RolCustomer)]
        public async Task<IActionResult> Post(SaleDtoRequest request)
        {
            var email = HttpContext.User.Claims.First(p=>p.Type==ClaimTypes.Email).Value;
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
    }
}
