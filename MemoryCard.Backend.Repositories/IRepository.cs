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
        TEntity GetById(string guid);

        TEntity FindFirst(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> FindRange(Func<TEntity, bool> predicate);

        // TODO Insert, Update, Delete

    }
}
