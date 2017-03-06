using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models;
using Azurite.Infrastructure.ResponseHandling;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class OrdersWorker : IOrdersWorker
    {
        private readonly IRepository<Order> _rep;
        private readonly IRepository<OrderStatus> _orderStatusRep;
        private readonly IRepository<Product> _productsRep;

        public OrdersWorker(IRepository<Order> rep, IRepository<OrderStatus> orderStatusRep, IRepository<Product> productsRep)
        {
            this._rep = rep;
            this._orderStatusRep = orderStatusRep;
            this._productsRep = productsRep;
        }

        public OrderW Get(Guid id)
        {
            var order = this._rep.Get(id);
            var orderW = Mapper.Map<OrderW>(order);

            return orderW;
        }

        public IQueryable<OrderW> GetAll()
        {
            return _rep.GetAll()
                .ProjectTo<OrderW>();
        }

        public IQueryable<OrderViewModel> GetAllVm()
        {
            return _rep.GetAll()
                .ProjectTo<OrderViewModel>();
        }

        public IList<OrderStatusW> GetOrderStatuses()
        {
            return this._orderStatusRep.GetAll()
                .ProjectTo<OrderStatusW>().ToList();
        }

        public ITicket Update(Guid orderId, int statusId, string notes)
        {
            var order = _rep.Get(orderId);

            if (order.StatusId != statusId)
            {
                var ticket = HandleProductQuantities(order, statusId);

                if (ticket.IsOK)
                {
                    order.StatusId = statusId;
                    _rep.Save();
                }

                return ticket;
            }
            else if (order.Notes != notes)
            {
                order.Notes = notes;
                _rep.Save();
            }

            return new Ticket(true);
        }

        private ITicket HandleProductQuantities(Order order, int statusId)
        {
            //if it was cancelled but then it got un-cancelled
            if (statusId != (int)OrderStatuses.Cancelled)
            {
                if (order.StatusId == (int)OrderStatuses.Cancelled)
                {
                    var products = _productsRep.GetAll();
                    foreach (var orderedProduct in order.OrderedProducts)
                    {
                        if (products.Any(p => p.Id == orderedProduct.ActualProductId))
                        {
                            //remove the ordered quantity from the actual product
                            var product = products.Single(p => p.Id == orderedProduct.ActualProductId);
                            product.Quantity -= orderedProduct.Quantity;

                            if (product.Quantity < 0)
                            {
                                return new Ticket(false, "Не е налично достатъчно количество за обработване на тази поръчка от продукт: " + product.Model);
                            }
                        }
                    }
                }
            }
            else
            {
                //was just cancelled
                var products = _productsRep.GetAll();
                foreach (var orderedProduct in order.OrderedProducts)
                {
                    if (products.Any(p => p.Id == orderedProduct.ActualProductId))
                    {
                        //add the ordered quantity back to the actual product
                        var product = products.Single(p => p.Id == orderedProduct.ActualProductId);
                        product.Quantity += orderedProduct.Quantity;
                    }
                }
            }

            return new Ticket(true);
        }
    }
}