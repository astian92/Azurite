using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Contracts
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity Get(TKey key);
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TKey key);
    }
}
