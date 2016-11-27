using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Wrappers;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Azurite.Storehouse.Models;
using Azurite.Infrastructure.ResponseHandling;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class OrdersWorker : IOrdersWorker
    {
        private readonly IRepository<Order> rep;
        private readonly IRepository<OrderStatus> orderStatusRep;
        private readonly IRepository<Product> productsRep;

        public OrdersWorker(IRepository<Order> rep, IRepository<OrderStatus> orderStatusRep,
            IRepository<Product> productsRep)
        {
            this.rep = rep;
            this.orderStatusRep = orderStatusRep;
            this.productsRep = productsRep;
        }

        public OrderW Get(Guid id)
        {
            var order = this.rep.Get(id);
            var orderW = Mapper.Map<OrderW>(order);

            return orderW;
        }

        public IQueryable<OrderW> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<OrderW>();
        }

        public IQueryable<OrderViewModel> GetAllVm()
        {
            return rep.GetAll()
                .ProjectTo<OrderViewModel>();
        }

        public IList<OrderStatusW> GetOrderStatuses()
        {
            return this.orderStatusRep.GetAll()
                .ProjectTo<OrderStatusW>().ToList();
        }

        public ITicket ChangeStatus(Guid orderId, int statusId)
        {
            var order = rep.Get(orderId);

            //if (order.StatusId != statusId)
            //{
            //    var ticket = HandleProductQuantities(order, statusId);

                //if (ticket.IsOK)
                //{
                    order.StatusId = statusId;
                    rep.Save();
                //}

            //    return ticket;
            //}

            return new Ticket(true);
        }

        //private ITicket HandleProductQuantities(Order order, int statusId)
        //{
        //    //if it was cancelled but then it got un-cancelled
        //    if (statusId != (int)OrderStatuses.Cancelled)
        //    {
        //        if (order.StatusId == (int)OrderStatuses.Cancelled)
        //        {
        //            var products = productsRep.GetAll();
        //            foreach (var orderedProduct in order.OrderedProducts)
        //            {
        //                if (products.Any(p => p.Id == orderedProduct.ActualProductId))
        //                {
        //                    //remove the ordered quantity from the actual product
        //                    var product = products.Single(p => p.Id == orderedProduct.ActualProductId);
        //                    product.Quantity -= orderedProduct.Quantity;

        //                    if (product.Quantity < 0)
        //                    {
        //                        return new Ticket(false, "Не е налично достатъчно количество за обработване на тази поръчка от продукт: " + product.Number);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else //was just cancelled
        //    {
        //        var products = productsRep.GetAll();
        //        foreach (var orderedProduct in order.OrderedProducts)
        //        {
        //            if (products.Any(p => p.Id == orderedProduct.ActualProductId))
        //            {
        //                //add the ordered quantity back to the actual product
        //                var product = products.Single(p => p.Id == orderedProduct.ActualProductId);
        //                product.Quantity += orderedProduct.Quantity;
        //            }
        //        }
        //    }

        //    return new Ticket(true);
        //}
    }
}