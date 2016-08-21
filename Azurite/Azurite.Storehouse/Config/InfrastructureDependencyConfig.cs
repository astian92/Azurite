using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.Data.Implementations;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models.Infrastructure;
using Azurite.Storehouse.Wrappers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Config
{
    public class InfrastructureDependencyConfig
    {
        public static void RegisterInfrastructure(IKernel kernel)
        {
            kernel.Bind<IDbFactory>()
                .To<StorehouseDbFactory>()
                .InTransientScope(); //make sure

            kernel.Bind<IStorehouseDbFactory>()
                .To<StorehouseDbFactory>()
                .InTransientScope();

            kernel.Bind<IRepository<Category, Guid>>()
                .To<Repository<Category, Guid>>()
                .InTransientScope();

            kernel.Bind<ITranslatedRepository<CategoryW, Guid>>()
                .To<TranslatedRepository<CategoryW, Category, Guid>>()
                .InTransientScope();
        }
    }
}