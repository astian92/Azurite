using Azurite.CDN.Config.Constants;
using Azurite.CDN.Config.Streamline;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Azurite.CDN.Config
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