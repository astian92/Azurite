using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Models.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Config
{
    public class InfrastructureDependencyConfig
    {
        public static void RegisterInfrastructure(IKernel kernel)
        {
            kernel.Bind<IDbFactory>()
                .To<StoreDbFactory>()
                .InTransientScope(); //make sure

            kernel.Bind<IStoreDbFactory>()
                .To<StoreDbFactory>()
                .InTransientScope(); //make sure


        }
    }
}