using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderStatusW : IMap, IMapFrom<OrderStatus>
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameEN { get; set; }

        public virtual ICollection<OrderW> Orders { get; set; }

        public OrderStatusW()
        {
            this.Orders = new HashSet<OrderW>();
        }
    }
}