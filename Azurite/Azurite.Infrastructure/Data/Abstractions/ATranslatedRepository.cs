using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Abstractions
{
    public abstract class ATranslatedRepository<TWrap, TEntity, TKey> : ITranslatedRepository<TWrap, TKey>
       where TWrap : IMap
    {
        protected readonly IRepository<TEntity, TKey> rep;

        public ATranslatedRepository(IRepository<TEntity, TKey> rep)
        {
            this.rep = rep;
        }

        public virtual void Add(TWrap wrapper)
        {
            var entity = Mapper.Map<TEntity>(wrapper);
            rep.Add(entity);
        }

        public void Delete(TKey key)
        {
            rep.Delete(key);
        }

        public void Edit(TWrap wrapper)
        {
            var entity = Mapper.Map<TEntity>(wrapper);
            rep.Edit(entity);
        }

        public TWrap Get(TKey key)
        {
            var entity = rep.Get(key);
            return Mapper.Map<TWrap>(entity);
        }

        public IQueryable<TWrap> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<TWrap>();
        }
    }
}
