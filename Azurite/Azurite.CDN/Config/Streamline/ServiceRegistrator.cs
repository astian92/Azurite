﻿using Azurite.CDN.Config.Constants;
using Ninject;
using System;
using System.Linq;
using System.Reflection;

namespace Azurite.CDN.Config.Streamline
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