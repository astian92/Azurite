using Azurite.Storehouse.Config.Constants;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Azurite.Storehouse.Config.Streamline
{
    public class ServiceRegistrator
    {
        public static void RegisterAllServices(IKernel kernel)
        {
            var registrators = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == NamespaceConstants.Config &&
                       !t.IsInterface &&
                       !t.IsAbstract &&
                       typeof(IServiceRegistrator).IsAssignableFrom(t));

            foreach (var t in registrators)
            {
                var instance = (IServiceRegistrator)Activator.CreateInstance(t);
                instance.RegisterServices(kernel);
            }
        }
    }
}