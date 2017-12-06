using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICustomersWorker
    {
        IQueryable<CustomerIndexViewModel> GetAll();
        CustomerW Get(Guid Id);
    }
}