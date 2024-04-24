using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Domain.Configuration;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Persistence;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<MusicStoreUserIdentity> _userManager;
        private readonly ILogger _logger;
        private readonly AppConfig _appConfig;

        public UserService(UserManager<MusicStoreUserIdentity> userManager, ILogger logger, IOptions<AppConfig> options)
        {
            _userManager = userManager;
            _logger = logger;
            _appConfig = options.Value;
        }
        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();
            try
            {
                var identity = await _userManager.FindByEmailAsync(request.UserName);
                if (identity == null)
                {
                    throw new SecurityException("Usuario no existe");

                }

                //Verificamos que no esté bloqueado el usuario
                if(await _userManager.IsLockedOutAsync(identity))
                {
                    throw new SecurityException($"Demasiados intentos fallidos para el usuario {identity.UserName}");
                }

                //Comprobar contraseña si no está bloqueado el usuario
                var result = await _userManager.CheckPasswordAsync(identity, request.Password);
                if (!result)
                {
                    response.Success = false;
                    response.ErrorMessage = "Clave incorrecta";
                    _logger.LogWarning($"Error de autenticación para el usuario {request.UserName}");
                    await _userManager.AccessFailedAsync(identity);
                    return response;
                }

                //Verificamos el rol del usuario
                var roles = await _userManager.GetRolesAsync(identity);
                var expiredDate = DateTime.Now.AddDays(1);
                //Devuelve informacion del usuario
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, request.UserName),
                    new Claim(ClaimTypes.Expiration, expiredDate.ToString("yyyy-MM-dd HH:mm:ss")),
                };

                //Devuelve roles a la lista claims
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                response.Roles = new List<string>();
                response.Roles.AddRange(roles);


                //Creacion del JWT
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Jwt.SecretKey));

                //Credenciales codificados en HmacSha256
                var credentials = new SigningCredentials(symmetricKey,SecurityAlgorithms.HmacSha256);
                //Header, encabezado
                var header = new JwtHeader(credentials);
                //Parte del medio del token
                var payload = new JwtPayload(
                    _appConfig.Jwt.Emisor, 
                    _appConfig.Jwt.Audiencia,
                    claims, 
                    notBefore: DateTime.Now,//momento de creacion del token
                    expires: expiredDate);

                var token = new JwtSecurityToken(header, payload);
                response.Token = new JwtSecurityTokenHandler().WriteToken(token); //me devuelve el token como un string
                response.FullName = $"{identity.FirstName} {identity.LastName}";
                response.Success = true ;


            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al autenticar al usuario";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public Task<BaseResponseGeneric<string>> RegisterAsync(RegisterDtoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
