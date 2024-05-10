using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using MusicStore.Persistence;
using MusicStore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MusicStore.Repositories.Implementations
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(MusicStoreDbContext context) : base(context)
        {
        }

        public async Task CreateTransactionAsync()
        {
            //Inicia transacción con nivel serializable
            await Context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }
        public override async Task<int> AddAsync(Sale entity)
        {
            entity.SaleDate = DateTime.Now;
            var lastNumber = await Context.Set<Sale>().CountAsync() + 1;
            entity.OperationNumber = $"{lastNumber:00000}";

            //Agregar identidad al context de forma implicita
            await Context.AddAsync(entity);
            return entity.Id;
        }
        public async override Task UpdateAsync(Sale? entity = null)
        {
            //Confirma transacción y luego actualiza la base de datos
            await Context.Database.CommitTransactionAsync();
            await base.UpdateAsync(entity);
        }

        public override async Task<Sale?> FindByIdAsync(int id)
        {
            //Eager Loading
            //return await Context.Set<Sale>()
            //    .Include(x => x.Customer)
            //    .Include(x => x.Concert)
            //    .ThenInclude(p => p.Genre)
            //    .Where(p => p.Id == id)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync();


            //Lazy Loading
            var query = Context.Set<Sale>()
                .Where(s => s.Id == id)
                .Select(p => new Sale
                {
                    Id = p.Id,
                    Customer = new Customer()
                    {
                        FullName = p.Customer.FullName
                    },
                    Concert = new Concert()
                    {
                        DateEvent = p.Concert.DateEvent,
                        ImageUrl = p.Concert.ImageUrl,
                        Title = p.Concert.Title,
                        Genre = new Genre()
                        {
                            Name = p.Concert.Genre.Name
                        },
                    },
                    OperationNumber = p.OperationNumber,
                    Quantity = p.Quantity,
                    SaleDate = p.SaleDate,
                    Total = p.Total,
                });

            return await query.FirstOrDefaultAsync();
        }

        public async Task RollBackAsync()
        {
            await Context.Database.RollbackTransactionAsync();
        }
        public async Task<IEnumerable<ReportInfo>> GetReportSaleAsync(DateTime dateStart, DateTime dateEnd)
        {

            //var query = Context.Set<ReportInfo>()
            //    .FromSqlRaw("EXEC uspReportSales {0},{1}", dateStart, dateEnd);

            //return await query.ToListAsync();

            //Uso de Dapper
            var query = await Context.Database.GetDbConnection()
                .QueryAsync<ReportInfo>(sql: "uspReportSales", commandType: CommandType.StoredProcedure,
                param: new
                {
                    DateStart = dateStart,
                    DateEnd = dateEnd
                });

            return query;
        }

    }
}
