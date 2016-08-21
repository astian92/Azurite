using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Abstractions
{
    public abstract class ARepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly DbContext db;

        public ARepository(IDbFactory factory)
        {
            db = factory.Create();
        }

        public virtual void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public virtual void Delete(TKey key)
        {
            var entity = this.Get(key);
            db.Set<TEntity>().Remove(entity);

            db.SaveChanges();
        }

        public virtual void Edit(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public virtual TEntity Get(TKey key)
        {
            var entity = db.Set<TEntity>()
                .Find(key);

            db.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }
    }
}
