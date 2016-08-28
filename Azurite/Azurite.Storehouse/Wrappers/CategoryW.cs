using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Azurite.Storehouse.Models;

namespace Azurite.Storehouse.Wrappers
{
    public class CategoryW : IMap, IMapFrom<Category>, IHaveCustomMappings
    {
        public System.Guid Id { get; set; }
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