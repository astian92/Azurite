using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IProductsWorker
    {
        IQueryable<ProductIndexViewModel> GetAll();
        List<DropDownItem> GetCategoriesDropDownItems();
        List<CategoryAttributeW> GetCategoryAttributes(Guid categoryId);
        ProductW Get(Guid productId);
        Task<ITicket> Add(ProductW productW, IEnumerable<HttpPostedFileBase> photos);
        Task<ITicket> Edit(ProductW productW, IEnumerable<HttpPostedFileBase> photos, IEnumerable<Guid> imageIds);
        ITicket Delete(Guid Id);
    }
}