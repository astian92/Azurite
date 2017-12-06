using Ninject;

namespace Azurite.Storehouse.Config.Streamline
{
    public interface IServiceRegistrator
    {
        void RegisterServices(IKernel kernel);
    }
}
