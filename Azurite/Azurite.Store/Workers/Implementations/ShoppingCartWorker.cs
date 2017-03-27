using AutoMapper;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Common;
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
        private const int ORDER_NUMBER_LENGTH = 7;

        private readonly IRepository<Product> rep;
        private readonly IRepository<Order> orderRep;
        private readonly IRepository<OrderStatus> orderStatusRep;

        public ShoppingCartWorker(IRepository<Product> rep, IRepository<Order> orderRep, IRepository<OrderStatus> orderStatusRep)
        {
            this.rep = rep;
            this.orderRep = orderRep;
            this.orderStatusRep = orderStatusRep;
        }

        public OrderedProductW GetProduct(Guid productId)
        {
            var product = rep.Get(productId);

            var orderedProduct = new OrderedProductW();
            orderedProduct.Id = Guid.NewGuid();
            orderedProduct.ActualProductId = product.Id;
            orderedProduct.ProductModel = product.Model;
            orderedProduct.ProductName = product.Name;
            orderedProduct.ProductNameEN = product.NameEN;
            orderedProduct.Price = product.Price;
            orderedProduct.Discount = product.Discount;
            orderedProduct.Quantity = product.Quantity;

            var images = new List<ProductImageW>();
            foreach (var image in product.ProductImages)
            {
                var imageW = Mapper.Map<ProductImageW>(image);
                images.Add(imageW);
            }

            orderedProduct.ProductImages = images;

            return orderedProduct;
        }

        public List<OrderedProductW> GetShoppingCart()
        {
            var cartProducts = HttpContext.Current.Session[CART_PRODUCTS_SESSION_NAME] as List<OrderedProductW>;

            if(cartProducts == null)
            {
                cartProducts = new List<OrderedProductW>();
                HttpContext.Current.Session[CART_PRODUCTS_SESSION_NAME] = cartProducts;
            }

            return cartProducts;
        }

        public void AddProduct(Guid productId, int quantity)
        {
            var product = GetProduct(productId);
            if(product.Quantity > 0)
            {
                var cartProducts = GetShoppingCart();
                if(cartProducts.Any(p => p.ActualProductId == productId))
                {
                    cartProducts.Single(p => p.ActualProductId == productId).Quantity += quantity;
                }
                else
                {
                    product.Quantity = quantity;
                    cartProducts.Add(product);
                }
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

        public void ModifyProductQty(Guid productId, int quantity)
        {
            var cartProducts = GetShoppingCart();
            var product = cartProducts.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Quantity = quantity;
            }
        }

        public OrderW GetCartSummary()
        {
            var cartProducts = GetShoppingCart();

            var orderW = new OrderW();
            orderW.OrderedProducts = cartProducts;

            return orderW;
        }

        public OrderW GetOrder()
        {
            var order = GetCartSummary();
            order.Id = Guid.NewGuid();
            order.Number = ApplicationHelpers.GenerateRandomString(ORDER_NUMBER_LENGTH);

            return order;
        }

        public bool SaveOrder(OrderW orderW)
        {
            var orderedProducts = GetShoppingCart();
            if (orderW != null && orderedProducts.Count() > 0)
            {
                orderW.Customer.Id = Guid.NewGuid();
                orderW.CustomerId = orderW.Customer.Id;
                orderW.OrderedProducts = orderedProducts;
                orderW.Date = DateTime.UtcNow;
                var orderStatuses = orderStatusRep.GetAll();
                orderW.StatusId = orderStatuses.OrderBy(x => x.Id).FirstOrDefault().Id;
                var currencyCours = ApplicationHelpers.GetCurrentCurrency();
                orderW.CurrencyId = currencyCours.Id;

                var order = Mapper.Map<Order>(orderW);
                try
                {
                    orderRep.Add(order);
                    orderRep.Save();
                    ReduceProductQuantity(orderW);
                    DisplaceOrder();
                    return true;
                }
                catch(Exception)
                {
                }
            }

            return false;
        }

        private void ReduceProductQuantity(OrderW order)
        {
            try
            {
                foreach (var orderedProduct in order.OrderedProducts)
                {
                    var product = rep.Get(orderedProduct.ActualProductId);
                    product.Quantity = product.Quantity - orderedProduct.Quantity;
                }

                rep.Save();
            }
            catch(Exception)
            {
            }
        }

        public void DisplaceOrder()
        {
            HttpContext.Current.Session[CART_PRODUCTS_SESSION_NAME] = null;
        }
    }
}