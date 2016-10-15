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

        IQueryable<CategoryW> GetBaseCategories();

        IQueryable<CategoryW> GetSubCategories(Guid categoryId);

        CategoryW GetCategory(Guid categoryId);

        IQueryable<CategoryAttributeW> GetCategoryAttr(Guid categoryId);
    }
}