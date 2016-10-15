using Azurite.Infrastructure.Data.Contracts;
using Azurite.CDN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Models.Infrastructure
{
    public interface ICdnDbFactory : IDbFactory
    {
        MarketPlaceEntities CreateConcrete();
    }
}