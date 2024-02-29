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
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        public ConcertRepository(MusicStoreDbContext context)
            :base(context)
        {
            
        }
    }
}
