using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IProductsWorker
    {
        IQueryable<ProductIndexViewModel> GetAll();
        List<DropDownItem> GetCategoriesDropDownItems();
    }
}