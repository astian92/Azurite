using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Contracts
{
    public interface IProductWorker
    {
        IQueryable<ProductW> GetProducts(Guid categoryId);
        ProductW GetProduct(Guid productId);
        IQueryable<CategoryW> GetSubCategories(Guid categoryId);
        IQueryable<ProductW> GetPromoProducts();
        IQueryable<ProductW> GetRelatedProducts(Guid categoryId);
    }
}