using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Interfaces
{
    public interface IConcertService
    {
        Task<BaseResponsePagination<ConcertDtoResponse>> ListAsync(string? filter, int page, int rows);

        Task<BaseResponseGeneric<ConcertDtoResponse>> FindByIdAsync(int id);

        Task<BaseResponseGeneric<int>> AddAsync(ConcertDtoRequest request);
        Task<BaseResponse> UpdateAsync(int id, ConcertDtoRequest request);
        Task<BaseResponse> DeleteAsync(int id);

    }
}
