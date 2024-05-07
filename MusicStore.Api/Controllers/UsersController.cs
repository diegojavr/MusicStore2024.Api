using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Services.Interfaces;
using System.Security.Claims;

namespace MusicStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDtoRequest request)
        {
            var response = await _service.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDtoRequest request)
        {
            var response = await _service.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> RequestTokenToResetPassword(ResetPasswordDtoRequest request)
        {
            var response = await _service.RequestTokenToResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ConfirmPasswordDtoRequest request)
        {
            var response = await _service.ResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDtoRequest request)
        {
            //Recuperamos primero el email de los claims del usuario autenticado 
            var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            var response = await _service.ChangePasswordAsync(email, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }

}
