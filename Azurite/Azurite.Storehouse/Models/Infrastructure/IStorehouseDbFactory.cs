using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Models.Infrastructure
{
    public interface IStorehouseDbFactory : IDbFactory
    {
        MarketPlaceEntities CreateConcrete();
    }
}