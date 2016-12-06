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
        private const string CART_ORDER_SESSION_NAME = "Order";

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

        public void RemoveProduct(Guid productId)
        {
            var cartProducts = GetShoppingCart();
            var product = cartProducts.FirstOrDefault(p => p.Id == productId);
            if(product != null)
            {
                cartProducts.Remove(product);
            }
        }

        public OrderW GetCartSummary()
        {
            var cartProducts = GetShoppingCart();

            var orderW = new OrderW();
            orderW.OrderedProducts = cartProducts;

            return orderW;
        }

        public bool CheckOutOrder(CustomerW customer)
        {
            var order = GetCartSummary();
            if(order.OrderedProducts != null && order.OrderedProducts.Count() > 0)
            {
                order.Id = Guid.NewGuid();
                order.Number = "SomeOrderNumber";
                order.Customer = customer;
                order.CustomerId = customer.Id;

                HttpContext.Current.Session[CART_ORDER_SESSION_NAME] = order;
                return true;
            }

            return false;
        }

        public OrderW GetOrder()
        {
            var order = GetCartSummary();
            order.Id = Guid.NewGuid();
            order.Number = "SomeOrderNumber";

            return order;
        }

        public bool SaveOrder(OrderW orderW)
        {
            if(orderW != null)
            {
                var order = Mapper.Map<Order>(orderW);
                order.Date = DateTime.UtcNow;

                var orderStatuses = orderStatusRep.GetAll();
                order.StatusId = orderStatuses.OrderBy(x => x.Id).FirstOrDefault().Id;

                try
                {
                    //orderRep.Add(order);
                    //orderRep.Save();
                    return true;
                }
                catch(Exception)
                {
                }
            }

            return false;
        }
    }
}