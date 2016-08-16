using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.NinjectConfigurations
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
            //_kernel.Bind<IDbFactory>()
            //    .To<DbFactory>()
            //    .InTransientScope();

            //_kernel.Bind<ITranslatedRepository<ProductW, Product, Guid>>()
            //    .To<GenericTranslatedRepository<ProductW, Product, Guid>>()
            //    .InTransientScope();

            //_kernel.Bind<IRepository<Product, Guid>>()
            //    .To<IRepository<Product, Guid>>()
            //    .InTransientScope();
        }
    }
}