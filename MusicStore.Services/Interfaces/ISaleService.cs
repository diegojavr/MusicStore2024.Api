using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Interfaces
{
    public interface ISaleService
    {
        Task<BaseResponseGeneric<int>> AddAsync(string email, SaleDtoRequest request);

        Task<BaseResponseGeneric<SaleDtoResponse>> FindByIdAsync(int id); 
    }
}
