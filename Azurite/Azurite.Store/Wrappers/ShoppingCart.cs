using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class ShoppingCart
    {
        public IEnumerable<ProductW> Products { get; set; }
        public double Total { get; set; }

        public ShoppingCart(IEnumerable<ProductW> products)
        {
            this.Products = products;
        }
    }
}