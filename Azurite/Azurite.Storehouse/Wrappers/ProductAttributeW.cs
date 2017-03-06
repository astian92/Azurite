using System;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductAttributeW : IMap, IMapFrom<ProductAttribute>
    {
        public Guid Id { get; set; }

        public Guid AttributeId { get; set; }

        public Guid ProductId { get; set; }

        public string Value { get; set; }

        public string ValueEN { get; set; }

        public virtual CategoryAttributeSimpleW CategoryAttribute { get; set; }
    }
}