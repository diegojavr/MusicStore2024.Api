using MusicStore.Domain;
using MusicStore.Domain.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Interfaces
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollBackAsync();
        Task<IEnumerable<ReportInfo>> GetReportSaleAsync(DateTime dataStart, DateTime dateTime);
    }
}
