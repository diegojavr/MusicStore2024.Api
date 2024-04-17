using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Services.Interfaces;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _service;
        public ConcertsController(IConcertService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string? filter, int page =1, int rows=5)
        {
            var response = await _service.ListAsync(filter, page, rows);
            return Ok(response);
        }
    }
}
