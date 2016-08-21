using Azurite.Infrastructure.Data.Abstractions;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Implementations
{
    public class TranslatedRepository<TWrap, TEntity, TKey> :
        ATranslatedRepository<TWrap, TEntity, TKey>
        where TWrap : IMap
    {
        public TranslatedRepository(IRepository<TEntity, TKey> rep)
            : base(rep)
        {

        }
    }
}
