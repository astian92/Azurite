using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class CategoryAttributeW : IMap, IMapFrom<CategoryAttribute>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeNameEN { get; set; }
        public bool ActiveFilter { get; set; }

        //public virtual CategoryW Category { get; set; }
        public virtual ICollection<ProductAttributeW> ProductAttributes { get; set; }

        public CategoryAttributeW()
        {
            this.ProductAttributes = new HashSet<ProductAttributeW>();
        }
    }
}