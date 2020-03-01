using Azurite.Infrastructure.Config;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Config.Streamline;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Config.NinjectConfig
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            ServiceRegistrator.RegisterAllServices(_kernel);
            AutoMapperConfig.RegisterMappings();
        }
    }
}