using MusicStore.Domain;
using MusicStore.Domain.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Interfaces
{
    public interface IConcertRepository : IRepositoryBase<Concert>
    {
        Task<(ICollection<ConcertInfo>Collection, int Total)> ListAsync(string? filter, int page, int rows);

        Task FinalizeAsync(int id);
    }
}
