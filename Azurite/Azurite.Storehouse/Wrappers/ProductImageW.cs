using System;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductImageW : IMap, IMapFrom<ProductImage>
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ImagePath { get; set; }
    }
}