using AutoMapper;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class CategoryW
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        //public string Description { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryW>()
                .ForMember(d => d.Name, opt => opt.MapFrom(
                        s => LanguageChecker.IsEnglish ? s.Name : s.Description));
        }
    }
}