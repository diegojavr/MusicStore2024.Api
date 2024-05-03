using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Interfaces
{
    public interface ICustomerRepository :IRepositoryBase<Customer>
    {
        Task<Customer?> FindByEmailAsync(string email);
        
    }
}
