using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Contracts
{
    public interface IShoppingCartWorker
    {
        List<OrderedProductW> GetShoppingCart();
        void AddProduct(Guid id, int quantity);
        void RemoveProduct(Guid id);
        OrderW GetCartSummary();
        OrderW GetOrder();
        bool CheckOutOrder(CustomerW customer);
        bool SaveOrder(OrderW order);
    }
}