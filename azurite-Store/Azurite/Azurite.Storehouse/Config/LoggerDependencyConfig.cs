using Azurite.Storehouse.Config.Streamline;
using log4net;
using Ninject;

namespace Azurite.Storehouse.Config
{
    public class LoggerDependencyConfig : IServiceRegistrator
    {
        public void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILog>()
                  .ToMethod(c => LogManager.GetLogger("Master"));
        }
    }
}