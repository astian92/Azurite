using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class OrderedProductW : IMap, IMapFrom<OrderedProduct>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ActualProductId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductNameEN { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}