using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Data.Implementations;
using Azurite.Storehouse.Config.Streamline;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models.Infrastructure;
using Azurite.Storehouse.Wrappers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Config
{
    public class InfrastructureDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbFactory>()
                .To<StorehouseDbFactory>()
                .InTransientScope();

            kernel.Bind<IStorehouseDbFactory>()
                .To<StorehouseDbFactory>()
                .InTransientScope();

            kernel.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InTransientScope();
        }
    }
}