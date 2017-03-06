using System;
using Ninject;

namespace Azurite.Storehouse
{
    public static class ServiceFactory
    {
        private static IKernel _kernel;

        public static void InitializeKernel(IKernel appKernel)
        {
            _kernel = appKernel;
        }

        public static T GetInstance<T>()
        {
            return _kernel.Get<T>();
        }

        public static object GetInstance(Type type)
        {
            return _kernel.Get(type);
        }

        public static T TryGetInstance<T>()
        {
            return _kernel.TryGet<T>();
        }

        public static object TryGetInstance(Type type)
        {
            return _kernel.TryGet(type);
        }
    }
}