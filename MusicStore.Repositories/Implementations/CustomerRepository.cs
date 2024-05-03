using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using MusicStore.Persistence;
using MusicStore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Implementations
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(MusicStoreDbContext context):base(context)
        {
            
        }
        public async Task<Customer?> FindByEmailAsync(string email)
        {
            return await Context.Set<Customer>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
