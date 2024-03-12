using MusicStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Interfaces
{
    public interface IGenreService
    {
        Task<BaseResponseGeneric<ICollection<GenreDtoResponse>>> ListAsync();


        Task<BaseResponseGeneric<GenreDtoResponse>> FindByIdAsync(int id);

        Task<BaseResponseGeneric<int>> AddAsync(GenreDtoResponse request);
        Task<BaseResponse> UpdateAsync (int id, GenreDtoResponse request);
        Task<BaseResponse> DeleteAsync (int id);
    }
}
