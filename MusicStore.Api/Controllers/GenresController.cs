using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Repositories.Interfaces;
using MusicStore.Services.Interfaces;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class GenresController: ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenreDtoRequest request)
        { 
            var response = await _service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreDtoRequest request)
        {
            var response = await _service.UpdateAsync(id,request);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);    
        }
    }
}
