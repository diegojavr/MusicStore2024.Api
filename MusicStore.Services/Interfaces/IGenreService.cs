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
    }
}
