using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Contracts
{
    public interface IShoppingCartWorker
    {
        List<ProductW> GetShoppingCart();
        void AddProduct(Guid id);
        void RemoveProduct(Guid id);
    }
}