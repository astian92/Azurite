using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Azurite.Storehouse.Wrappers
{
    public class CategoryAttributeSimpleW : IMap, IMapFrom<CategoryAttribute>, IMapFrom<CategoryAttributeW>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeNameEN { get; set; }
        public bool ActiveFilter { get; set; }
    }
}