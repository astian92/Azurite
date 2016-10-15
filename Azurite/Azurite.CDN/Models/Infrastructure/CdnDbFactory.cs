using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Azurite.CDN.Data;
using Azurite.CDN.Models.Infrastructure;

namespace Azurite.Store.Models.Infrastructure
{
    public class CdnDbFactory : ICdnDbFactory
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