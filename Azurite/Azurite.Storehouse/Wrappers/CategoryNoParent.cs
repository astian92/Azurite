using System;
using AutoMapper;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class CategoryIndexViewModel : IMap, IMapFrom<Category>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameEN { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string ParentName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryIndexViewModel>()
                .ForMember(d => d.ParentName, conf => conf.MapFrom(s => s.Category1.Name));
        }
    }
}