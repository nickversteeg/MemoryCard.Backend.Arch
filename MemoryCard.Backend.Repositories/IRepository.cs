using MemoryCard.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        TEntity GetById(string guid);
        Task<TEntity> GetByIdAsync(string guid);

        TEntity FindFirst(Func<TEntity, bool> predicate);
        Task<TEntity> FindFirstAsync(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> FindRange(Func<TEntity, bool> predicate);
        Task<IEnumerable<TEntity>> FindRangeAsync(Func<TEntity, bool> predicate);

        // TODO Insert, Update, Delete

    }
}
