using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductIndexViewModel : IMap, IMapFrom<Product>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public int Active { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductIndexViewModel>()
                .ForMember(d => d.CategoryName, conf => conf.MapFrom(s => s.Category.Name));
        }
    }
}