using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Azurite.Store.Wrappers
{
    public class ProductW : IMap, IMapFrom<Product>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public int Active { get; set; }
        public string NameEN { get; set; }
        public string DescriptionEN { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            throw new NotImplementedException();
        }
    }
}