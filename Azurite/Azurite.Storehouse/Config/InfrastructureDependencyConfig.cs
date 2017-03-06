using System.Web;
using Ninject;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Data.Implementations;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Config.Streamline;
using Azurite.Storehouse.Models.Infrastructure;

namespace Azurite.Storehouse.Config
{
    public class InfrastructureDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITicket>()
                .To<Ticket>()
                .InTransientScope();

            kernel.Bind<IDbFactory>()
                .To<StorehouseDbFactory>()
                .InScope((f) => HttpContext.Current); //so we can scope all repositories to the same context

            kernel.Bind<IStorehouseDbFactory>()
                .To<StorehouseDbFactory>()
                .InScope((f) => HttpContext.Current); //so we can scope all repositories to the same context

            kernel.Bind(typeof(IRepository<>))
                .To(typeof(Repository<>))
                .InTransientScope();
        }
    }
}