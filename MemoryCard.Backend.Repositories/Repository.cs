using MemoryCard.Backend.Database;
using MemoryCard.Backend.Models;
using Microsoft.EntityFrameworkCore;
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
        readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>()
                .ToListAsync();
        }

        public TEntity GetById(string guid)
        {
            return _dbContext.Set<TEntity>()
                .Where(entity => entity.Guid == guid)
                .FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(string guid)
        {
            return await Task.Run(_dbContext.Set<TEntity>()
                .Where(entity => entity.Guid == guid)
                .FirstOrDefault);
        }

        public TEntity FindFirst(Func<TEntity, bool> predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault();
        }
        public async Task<TEntity> FindFirstAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Run(_dbContext.Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault);
        }

        public IEnumerable<TEntity> FindRange(Func<TEntity, bool> predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(predicate);
        }
        public async Task<IEnumerable<TEntity>> FindRangeAsync(Func<TEntity, bool> predicate)
        {
            return await _dbContext.Set<TEntity>()
                .Where(predicate)
                .AsQueryable()
                .ToListAsync();
        }


    }
}
