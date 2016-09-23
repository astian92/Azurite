using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Contracts
{
    public interface ICategoryWorker
    {
        IQueryable<CategoryW> GetAll();
    }
}