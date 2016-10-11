using Azurite.Infrastructure.ResponseHandling;
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
        List<CategoryAttributeW> GetCategoryAttributes(Guid categoryId);
        ProductW Get(Guid productId);
        void Add(ProductW productW);
        void Edit(ProductW productW);
        ITicket Delete(Guid Id);
    }
}