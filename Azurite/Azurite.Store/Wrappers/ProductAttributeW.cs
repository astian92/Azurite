using AutoMapper;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
using Azurite.Store.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class ProductAttributeW : IMap, IMapFrom<ProductAttribute>
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ProductAttribute, ProductAttributeW>()
                .ForMember(d => d.Value, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.Value : s.ValueEN))
                .MaxDepth(2)
                .ReverseMap();
        }
    }
}