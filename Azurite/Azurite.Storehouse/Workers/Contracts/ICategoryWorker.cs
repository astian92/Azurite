using Azurite.Storehouse.Data;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICategoryWorker
    {
        IQueryable<CategoryW> GetAll();
        void Add(CategoryW categoryW);
    }
}