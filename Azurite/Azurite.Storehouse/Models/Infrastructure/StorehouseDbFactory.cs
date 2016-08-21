using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Models.Infrastructure
{
    public class StorehouseDbFactory : IStorehouseDbFactory
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