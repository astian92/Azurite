using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Data.Implementations;
using Azurite.Store.Config.Streamline;
using Azurite.Store.Models.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Config
{
    public class InfrastructureDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbFactory>()
                .To<StoreDbFactory>()
                .InTransientScope();

            kernel.Bind<IStoreDbFactory>()
                .To<StoreDbFactory>()
                .InTransientScope();

            kernel.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InTransientScope();
        }
    }
}