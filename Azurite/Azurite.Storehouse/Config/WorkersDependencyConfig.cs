using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Azurite.Storehouse.Config.Streamline;
using Ninject.Extensions.Conventions;
using Azurite.Storehouse.Config.Constants;

namespace Azurite.Storehouse.Config
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