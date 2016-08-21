using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.Infrastructure
{
    public interface IStorehouseDbFactory : IDbFactory
    {
        MarketPlaceEntities CreateConcrete();
    }
}