using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IUsersWorker
    {
        UserW Get(Guid Id);
        IQueryable<UserW> GetAll();
        void Add(UserW userW);
        void Edit(UserW userW);
        void Delete(Guid Id);
    }
}