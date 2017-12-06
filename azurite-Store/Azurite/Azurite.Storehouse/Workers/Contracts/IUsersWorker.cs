using Azurite.Infrastructure.ResponseHandling;
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
        ITicket Add(UserW userW);
        ITicket Edit(UserW userW);
        ITicket Delete(Guid Id);
    }
}