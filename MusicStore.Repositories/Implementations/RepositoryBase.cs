using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MusicStore.Domain;
using MusicStore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Implementations
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected readonly DbContext Context;

        protected RepositoryBase(DbContext context)
        {
            Context = context;
        }


        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity != null)
            {
                entity.Status = false;
                await UpdateAsync();
            }
            else
            {
                throw new InvalidOperationException($"No se encontró el registro en el Id {id}");
            }
        }

        public virtual async Task<TEntity?> FindByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);

        }

        public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await Context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task UpdateAsync(TEntity? entity = default)
        {

            if (entity != null)
            {
                entity.ModifiedDate = DateTime.Now;
                await UpdateAsync();
            }
            await Context.SaveChangesAsync();
        }
    }
}
