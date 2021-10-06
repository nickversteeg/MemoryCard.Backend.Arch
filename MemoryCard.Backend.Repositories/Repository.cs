using MemoryCard.Backend.Database;
using MemoryCard.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Repositories
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        MemoryCardDbContext dbContext;

        public IEnumerable<TEntity> FindRange(Func<TEntity, bool> predicate)
        {
            return dbContext.Set<TEntity>()
                .Where(predicate);
        }

        public TEntity FindFirst(Func<TEntity, bool> predicate)
        {
            return dbContext.Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>();
        }

        public TEntity GetById(string guid)
        {
            return dbContext.Set<TEntity>()
                .Where(entity => entity.Guid == guid)
                .FirstOrDefault();
        }
    }
}
