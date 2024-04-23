using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicStore.Domain.Configuration;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Persistence;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    internal class UserService : IUserService
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
        public Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseGeneric<string>> RegisterAsync(RegisterDtoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
