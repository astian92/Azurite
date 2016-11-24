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
    public class CategoryW : IMap, IMapFrom<Category>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<CategoryAttributeW> CategoryAttributes { get; set; }
        //public virtual ICollection<ProductW> Products { get; set; }

        public CategoryW()
        {
            //this.CategoryAttributes = new HashSet<CategoryAttributeW>();
            //this.Products = new HashSet<ProductW>();
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryW>()
                .ForMember(d => d.Name, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.Name : s.NameEN))
                .ForMember(d => d.Description, conf => conf.MapFrom(s => LanguageHelper.GetCurrentLanguage() == Language.BG ? s.Description : s.DescriptionEN))
                .MaxDepth(2)
                .ReverseMap();
        }
    }
}