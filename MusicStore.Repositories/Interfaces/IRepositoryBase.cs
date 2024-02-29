using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase //se puede utilizar unicamente con clases que hereden de EntityBase
    {
        //solo para enlistar elementos, por eso se usa ICollection
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> ListAsync();
        Task<int> AddAsync(TEntity entity);
        Task<TEntity?> FindByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
