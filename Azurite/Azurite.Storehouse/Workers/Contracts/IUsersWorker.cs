using System;
using System.Linq;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;

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