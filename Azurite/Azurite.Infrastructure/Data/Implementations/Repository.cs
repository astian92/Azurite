using Azurite.Infrastructure.Data.Abstractions;
using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Infrastructure.Data.Implementations
{
    public class Repository<TEntity, TKey> : ARepository<TEntity, TKey>
        where TEntity : class
    {
        public Repository(IDbFactory factory)
            : base(factory)
        {

        }
    }
}
