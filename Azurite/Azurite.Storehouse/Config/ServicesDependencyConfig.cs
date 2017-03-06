using Ninject;
using Ninject.Extensions.Conventions;
using Azurite.Storehouse.Config.Streamline;
using Azurite.Storehouse.Config.Constants;

namespace Azurite.Storehouse.Config
{
    public class ServicesDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x => x
                .From(GetType().Assembly)
                .SelectAllClasses().InNamespaces(NamespaceConstants.Services)
                .BindAllInterfaces()
                .Configure(b => b.InTransientScope()));
        }
    }
}