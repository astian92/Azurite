using Azurite.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Contracts
{
    public interface ITranslatedRepository<TWrap, TKey>
        where TWrap : IMap
    {
        TWrap Get(TKey key);
        IQueryable<TWrap> GetAll();
        void Add(TWrap wrapper);
        void Edit(TWrap wrapper);
        void Delete(TKey key);
    }
}
