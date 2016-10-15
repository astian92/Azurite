using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
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
        public string ValueEN { get; set; }

        public virtual ProductW Product { get; set; }
    }
}