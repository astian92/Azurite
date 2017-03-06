using System.Collections.Generic;
using System.Linq;

namespace Azurite.Infrastructure.Data.Contracts
{
    public interface IRepository<TEntity>
    {
        TEntity Get<TKey>(TKey key);

        IQueryable<TEntity> GetAll();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove<TKey>(TKey key);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Save();
    }
}
