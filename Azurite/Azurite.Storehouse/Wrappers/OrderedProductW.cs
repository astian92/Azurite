using System;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderedProductW : IMap, IMapFrom<OrderedProduct>
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ActualProductId { get; set; }

        public string ProductModel { get; set; }

        public string ProductName { get; set; }

        public string ProductNameEN { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public double Total { get; set; }
    }
}