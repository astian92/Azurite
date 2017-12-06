using Ninject;

namespace Azurite.CDN.Config.Streamline
{
    public interface IServiceRegistrator
    {
        void RegisterServices(IKernel kernel);
    }
}