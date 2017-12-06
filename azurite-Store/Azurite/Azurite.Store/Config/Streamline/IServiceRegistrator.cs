using Ninject;

namespace Azurite.Store.Config.Streamline
{
    public interface IServiceRegistrator
    {
        void RegisterServices(IKernel kernel);
    }
}