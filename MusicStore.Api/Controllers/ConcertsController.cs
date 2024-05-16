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
    [Authorize]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _service;
        public ConcertsController(IConcertService service)
        {
            _service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string? filter, int page = 1, int rows = 5)
        {
            
            var response = await _service.ListAsync(filter, page, rows);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> Post([FromBody] ConcertDtoRequest request)
        {
            var response = await _service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);

        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> Put(int id, [FromBody] ConcertDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id)
        {
            var response = await _service.FinalizeAsync(id);
            return Ok(response);
        }

    }
}
