using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class ProductImageW : IMap, IMapFrom<ProductImage>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImagePath { get; set; }

        public virtual ProductW Product { get; set; }
    }
}