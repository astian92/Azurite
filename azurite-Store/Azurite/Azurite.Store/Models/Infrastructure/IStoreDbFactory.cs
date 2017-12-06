using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Models.Infrastructure
{
    public interface IStoreDbFactory : IDbFactory
    {
        MarketPlaceEntities CreateConcrete();
    }
}