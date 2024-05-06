using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MusicStore.Domain;
using MusicStore.Domain.Configuration;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Persistence;
using MusicStore.Repositories.Interfaces;
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
        private readonly ILogger<UserService> _logger;
        private readonly AppConfig _appConfig;
        private readonly ICustomerRepository _repository;

        public UserService(UserManager<MusicStoreUserIdentity> userManager, ILogger<UserService> logger, IOptions<AppConfig> options, ICustomerRepository repository)
        {
            _userManager = userManager;
            _logger = logger;
            _appConfig = options.Value;
            _repository = repository;


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
                if (await _userManager.IsLockedOutAsync(identity))
                {
                    throw new SecurityException($"Demasiados intentos fallidos para el usuario {identity.UserName}");
                }

                //Comprobar contraseña si no está bloqueado el usuario
                var result = await _userManager.CheckPasswordAsync(identity, request.Password);
                if (!result)
                {
                    response.Success = false;
                    response.ErrorMessage = "Clave incorrecta";
                    _logger.LogWarning("Error de autenticación para el usuario {UserName}", request.UserName);
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

                //Devuelve roles a la lista claims, asigna el rol para el token
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                response.Roles = new List<string>();
                response.Roles.AddRange(roles);
                await _userManager.ResetAccessFailedCountAsync(identity);



                //JWT 
                //Creacion del JWT
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Jwt.SecretKey));

                //Credenciales codificados en HmacSha256
                var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
                //Header, encabezado
                var header = new JwtHeader(credentials);
                //Parte del medio del token
                var payload = new JwtPayload(
                    _appConfig.Jwt.Emisor,
                    _appConfig.Jwt.Audiencia,
                    claims,
                    notBefore: DateTime.Now,//momento de creacion del token
                    expires: expiredDate);

                //Unificacion del token
                var token = new JwtSecurityToken(header, payload);
                response.Token = new JwtSecurityTokenHandler().WriteToken(token); //me devuelve el token como un string
                response.FullName = $"{identity.FirstName} {identity.LastName}";
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al autenticar al usuario";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<string>> RegisterAsync(RegisterDtoRequest request)
        {
            var response = new BaseResponseGeneric<string>();
            try
            {
                var user = new MusicStoreUserIdentity()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    DocumentNumber = request.DocumentNumber,
                    DocumentType = (DocumentTypeEnum)request.DocumentType,
                    EmailConfirmed = true
                };
                //enviamos instancia de creacion de usuario y contraseña de confirmación
                var result = await _userManager.CreateAsync(user, request.ConfirmPassword);
                if (result.Succeeded)
                {
                    var userIdentity = await _userManager.FindByEmailAsync(request.Email);
                    if (userIdentity != null)
                    {
                        //Esto es para antes de crear el usuario, ya tenga un rol asignado
                        await _userManager.AddToRoleAsync(userIdentity, Constantes.RolCustomer);

                        //Crear objeto de usuario encontrado
                        var customer = new Customer()
                        {
                            Email = request.Email,
                            FullName = $"{request.FirstName} {request.LastName}"

                        };
                        //Agrega customer si fue encontrado correctamente
                        await _repository.AddAsync(customer);

                        //Enviar email de bienvenida o de confirmación
                        //

                        response.Success = true;
                        response.Data = userIdentity.Id;
                    }
                }
                //Si el usuairo no cumple politicas, o ya existe, etc
                else
                {
                    response.Success = false;
                    //Creamos un StringBuilder para concatenar todos los errores posibles
                    var sb = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        sb.AppendLine(error.Description);
                    }
                    response.ErrorMessage = sb.ToString();
                    sb.Clear(); //Limpiamos memoria de errores de sb
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al registrar el usuario";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                throw;
            }
            return response;
        }

        public async Task<BaseResponseGeneric<string>> RequestTokenToResetPasswordAsync(ResetPasswordDtoRequest request)
        {
            var response = new BaseResponseGeneric<string>();
            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Email); 
                if (userIdentity == null)
                {
                    throw new SecurityException("Usuario no existe");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);
                //TODO: enviar un email con el token para reseteo de contraseña
                response.Data = token;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al solicitar el token para resetear la clave";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                
            }

            return response;
        }
        public async Task<BaseResponse> ResetPasswordAsync(ConfirmPasswordDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Email);
                if (userIdentity == null)
                {
                    throw new SecurityException("Usuario no existe");
                }
                var result = await _userManager.ResetPasswordAsync(userIdentity, request.Token, request.ConfirmPassword);
                response.Success = result.Succeeded;

                if (!result.Succeeded)
                {
                    //Creamos un StringBuilder para concatenar todos los errores posibles
                    var sb = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        sb.AppendLine(error.Description);
                    }
                    response.ErrorMessage = sb.ToString();
                    sb.Clear(); //Limpiamos memoria de errores de sb}


                }
                else
                {
                    //TODO: enviar email de confirmacion de clave cambiada
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al resetear la clave";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

                return response;
        }
    }
}
