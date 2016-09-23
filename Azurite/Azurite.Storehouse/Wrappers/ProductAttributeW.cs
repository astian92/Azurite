using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductAttributeW : IMap, IMapFrom<ProductAttribute>
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }
        public string ValueEN { get; set; }

        //public virtual CategoryAttributeW CategoryAttribute { get; set; }
        public virtual ProductW Product { get; set; }
    }
}