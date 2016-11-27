using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Azurite.Store.Models.Helpers;

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
        public int Active { get; set; }

        public int Quantity { get; set; } //not mapping!

        public virtual CategoryW Category { get; set; }
        public virtual ICollection<ProductAttributeW> ProductAttributes { get; set; }
        public virtual ICollection<ProductImageW> ProductImages { get; set; }

        public ProductW()
        {
            this.ProductAttributes = new HashSet<ProductAttributeW>();
            this.ProductImages = new HashSet<ProductImageW>();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Product, ProductW>()
                .ForMember(d => d.Name, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.Name : s.NameEN))
                .ForMember(d => d.Description, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.Description : s.DescriptionEN))
                .ReverseMap();
        }
    }
}