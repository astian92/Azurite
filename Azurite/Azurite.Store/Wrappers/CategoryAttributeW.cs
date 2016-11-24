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
    public class CategoryAttributeW : IMap, IMapFrom<CategoryAttribute>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string AttributeName { get; set; }
        public bool ActiveFilter { get; set; }

        public virtual ICollection<ProductAttributeW> ProductAttributes { get; set; }

        public CategoryAttributeW()
        {
            this.ProductAttributes = new HashSet<ProductAttributeW>();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CategoryAttribute, CategoryAttributeW>()
                .ForMember(d => d.AttributeName, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.AttributeName : s.AttributeNameEN))
                .MaxDepth(2)
                .ReverseMap();
        }
    }
}