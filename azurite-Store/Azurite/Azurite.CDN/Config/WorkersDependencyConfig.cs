using Azurite.CDN.Config.Streamline;
using Ninject;
using Ninject.Extensions.Conventions;
using Azurite.CDN.Config.Constants;

namespace Azurite.CDN.Config
{
    public class WorkersDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x => x
                .From(GetType().Assembly)
                .SelectAllClasses().InNamespaces(NamespaceConstants.Workers)
                .BindAllInterfaces()
                .Configure(b => b.InTransientScope()));
        }
    }
}