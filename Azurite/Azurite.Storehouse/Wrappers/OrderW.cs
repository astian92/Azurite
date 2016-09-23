using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderW : IMap, IMapFrom<Order>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int StatusId { get; set; }
        public double Total { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public virtual CustomerW Customer { get; set; }
        public virtual ICollection<OrderedProductW> OrderedProducts { get; set; }
        public virtual OrderStatusW OrderStatus { get; set; }

        public OrderW()
        {
            this.OrderedProducts = new HashSet<OrderedProductW>();
        }
    }
}