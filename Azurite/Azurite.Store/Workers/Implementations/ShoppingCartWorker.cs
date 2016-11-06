using AutoMapper;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Implementations
{
    public class ShoppingCartWorker : IShoppingCartWorker
    {
        private const string CART_PRODUCTS_SESSION_NAME = "CartProducts";

        private readonly IRepository<Product> rep;

        public ShoppingCartWorker(IRepository<Product> rep)
        {
            this.rep = rep;
        }

        public ProductW GetProduct(Guid productId)
        {
            var product = rep.Get(productId);
            var productW = Mapper.Map<ProductW>(product);
            return productW;
        }

        public List<ProductW> GetShoppingCart()
        {
            var cartProducts = HttpContext.Current.Session[CART_PRODUCTS_SESSION_NAME] as List<ProductW>;

            if(cartProducts == null)
            {
                cartProducts = new List<ProductW>();
                HttpContext.Current.Session[CART_PRODUCTS_SESSION_NAME] = cartProducts;
            }

            return cartProducts;
        }

        public void AddProduct(Guid productId, int quantity)
        {
            var product = GetProduct(productId);
            var cartProducts = GetShoppingCart();
            if(cartProducts.Any(p => p.Id == productId))
            {
                cartProducts.Single(p => p.Id == productId).Quantity += 1;
            }
            else
            {
                product.Quantity = quantity;
                cartProducts.Add(product);
            }
        }

        public void RemoveProduct(Guid productId)
        {
            var cartProducts = GetShoppingCart();
            var product = cartProducts.FirstOrDefault(p => p.Id == productId);
            if(product != null)
            {
                cartProducts.Remove(product);
            }
        }
    }
}