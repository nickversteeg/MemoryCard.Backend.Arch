using MemoryCard.Backend.Database;
using MemoryCard.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Repositories
{
    // TODO Make async methods asynchronous
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        MemoryCardDbContext dbContext;

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return GetAll();
        }

        public TEntity GetById(string guid)
        {
            return dbContext.Set<TEntity>()
                .Where(entity => entity.Guid == guid)
                .FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(string guid)
        {
            return GetById(guid);
        }

        public TEntity FindFirst(Func<TEntity, bool> predicate)
        {
            return dbContext.Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault();
        }
        public async Task<TEntity> FindFirstAsync(Func<TEntity, bool> predicate)
        {
            return FindFirst(predicate);
        }

        public IEnumerable<TEntity> FindRange(Func<TEntity, bool> predicate)
        {
            return dbContext.Set<TEntity>()
                .Where(predicate);
        }
        public async Task<IEnumerable<TEntity>> FindRangeAsync(Func<TEntity, bool> predicate)
        {
            return FindRange(predicate);
        }



    }
}
