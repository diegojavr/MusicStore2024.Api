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
        //Task<BaseResponseGeneric<ICollection<GenreDtoResponse>>> ListAsync();


        //Task<BaseResponseGeneric<GenreDtoResponse>> FindByIdAsync(int id);

        //Task<BaseResponseGeneric<int>> AddAsync(GenreDtoRequest request);
        //Task<BaseResponse> UpdateAsync(int id, GenreDtoRequest request);
        //Task<BaseResponse> DeleteAsync(int id);

    }
}
