using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Azurite.Store.Data;

namespace Azurite.Store.Models.Infrastructure
{
    public class StoreDbFactory : IStoreDbFactory
    {
        public DbContext Create()
        {
            return new MarketPlaceEntities();
        }

        public MarketPlaceEntities CreateConcrete()
        {
            return (MarketPlaceEntities)this.Create();
        }
    }
}