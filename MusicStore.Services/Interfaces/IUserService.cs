using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
        Task<BaseResponseGeneric<string>> RegisterAsync(RegisterDtoRequest request);
        Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordDtoRequest request);
        Task<BaseResponse> ResetPasswordAsync(ConfirmPasswordDtoRequest request);
        Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordDtoRequest request);
    }
}
