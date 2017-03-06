using Azurite.Infrastructure.Data.Abstractions;
using Azurite.Infrastructure.Data.Contracts;

namespace Azurite.Infrastructure.Data.Implementations
{
    public class Repository<TEntity> : ARepository<TEntity>
        where TEntity : class
    {
        public Repository(IDbFactory factory)
            : base(factory)
        {
        }
    }
}
