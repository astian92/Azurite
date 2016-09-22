using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICategoriesWorker
    {
        CategoryW Get(Guid Id);
        IQueryable<CategoryW> GetAll();
        IQueryable<CategoryIndexViewModel> GetAllWithoutParents();
        void Add(CategoryW categoryW);
        void Edit(CategoryW categoryW);
        ITicket Delete(Guid Id);
    }
}