using System;
using System.Linq;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICustomersWorker
    {
        IQueryable<CustomerIndexViewModel> GetAll();

        CustomerW Get(Guid Id);
    }
}