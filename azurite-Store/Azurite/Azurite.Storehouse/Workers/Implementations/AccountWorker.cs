using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Models.ViewModels;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Models.Infrastructure;
using System.Web.Security;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class AccountWorker : IAccountWorker
    {
        private MarketPlaceEntities db;

        public AccountWorker(IStorehouseDbFactory factory)
        {
            this.db = factory.CreateConcrete();
        }

        public bool Authenticate(LoginViewModel model)
        {
            return db.Users.Any(u => u.Username == model.Username &&
                u.Password == model.Password);
        }
    }
}