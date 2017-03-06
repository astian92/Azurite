using System.Linq;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Models.ViewModels;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models.Infrastructure;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class AccountWorker : IAccountWorker
    {
        private MarketPlaceEntities _db;

        public AccountWorker(IStorehouseDbFactory factory)
        {
            this._db = factory.CreateConcrete();
        }

        public bool Authenticate(LoginViewModel model)
        {
            return _db.Users.Any(u => u.Username == model.Username &&
                u.Password == model.Password);
        }
    }
}