using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using MusicStore.Persistence;
using MusicStore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Implementations
{
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        private readonly IMapper _mapper;

        public ConcertRepository(MusicStoreDbContext context, IMapper mapper)
            :base(context)
        {
            _mapper = mapper;
        }

        public async Task<(ICollection<ConcertInfo> Collection, int Total)> ListAsync(string? filter, int page, int rows)
        {
            Expression<Func<Concert, bool>> predicate = p => p.Status && p.Title.Contains(filter ?? string.Empty);
            Expression<Func<Concert, string>> orderBy = p => p.Title; //ordena alfabeticamente el listado de conciertos
            //Expression<Func<Concert, ConcertInfo>> selector= p => _mapper.Map<ConcertInfo>(p);
            //eager loading

            //var collection = await Context.Set<Concert>()
            //    .Include(p=>p.Genre)
            //    .Where(predicate)
            //    .OrderBy(orderBy)
            //    .Skip((page - 1) * rows)
            //    .Take(rows)
            //    .Select(selector)
            //    .ToListAsync(); //devuelve la coleccion

            //var total = await Context.Set<Concert>()
            //    .Where(predicate)
            //    .CountAsync();
            //return (collection,total);

            //Lazy loading
            return await base.ListAsync(predicate, p => new ConcertInfo()
            {
                Id = p.Id,
                Title = p.Title,
                Genre = p.Genre.Name,
                Description = p.Description,
                Place = p.Place,
                UnitPrice = p.UnitPrice,
                DateEvent = p.DateEvent.ToString("yyyy-MM-dd"),
                TimeEvent = p.DateEvent.ToString("HH:mm"),
                TicketsQuantity = p.TicketsQuantity,
                ImageUrl = p.ImageUrl,
            }, orderBy, page, rows);

            
        }
        public override async Task<Concert?> FindByIdAsync(int id)
        {
            return await Context.Set<Concert>()
                .Include(p => p.Genre)
                .FirstOrDefaultAsync(p=> p.Id == id);
        }
    }
}
