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

namespace Azurite.Storehouse.Workers.Implementations
{
    public class OrdersWorker : IOrdersWorker
    {
        private readonly IRepository<Order> rep;
        private readonly IRepository<OrderStatus> orderStatusRep;

        public OrdersWorker(IRepository<Order> rep, IRepository<OrderStatus> orderStatusRep)
        {
            this.rep = rep;
            this.orderStatusRep = orderStatusRep;
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

        public void ChangeStatus(Guid orderId, int statusId)
        {
            var order = rep.Get(orderId);
            order.StatusId = statusId;

            rep.Save();
        }
    }
}