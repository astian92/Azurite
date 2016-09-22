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
        private MarketPlaceEntities context;

        public DbContext Create()
        {
            if (context != null)
            {
                return context;
            }

            // so it gets a scoped context ;)
            context = new MarketPlaceEntities();
            return context;
        }

        public MarketPlaceEntities CreateConcrete()
        {
            return (MarketPlaceEntities)this.Create();
        }
    }
}