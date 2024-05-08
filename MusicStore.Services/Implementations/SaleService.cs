using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services.Implementations
{
    internal class SaleService : ISaleService
    {
        public SaleService()
        {
            
        }
        public Task<BaseResponseGeneric<int>> AddAsync(string email, SaleDtoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
