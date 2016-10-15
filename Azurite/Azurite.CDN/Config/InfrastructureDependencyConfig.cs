using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Data.Implementations;
using Azurite.CDN.Config.Streamline;
using Azurite.CDN.Models.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Store.Models.Infrastructure;
using Azurite.Infrastructure.ResponseHandling;

namespace Azurite.CDN.Config
{
    public class InfrastructureDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITicket>()
                .To<Ticket>()
                .InTransientScope();

            kernel.Bind<IDbFactory>()
                .To<CdnDbFactory>()
                .InTransientScope();

            kernel.Bind<ICdnDbFactory>()
                .To<CdnDbFactory>()
                .InTransientScope();

            kernel.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InTransientScope();
        }
    }
}