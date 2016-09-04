using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class ProductW : IMap, IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public int Active { get; set; }

        public virtual CategoryW Category { get; set; }
        public virtual ICollection<ProductAttributeW> ProductAttributes { get; set; }
        public virtual ICollection<ProductImageW> ProductImages { get; set; }

        public ProductW()
        {
            this.ProductAttributes = new HashSet<ProductAttributeW>();
            this.ProductImages = new HashSet<ProductImageW>();
        }
    }
}