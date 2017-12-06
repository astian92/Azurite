using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Common;
using Azurite.Store.Data;
using Azurite.Store.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class OrderedProductW : IMap, IMapFrom<OrderedProduct>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ActualProductId { get; set; }
        public string ProductModel { get; set; }
        public string ProductName { get; set; }
        public string ProductNameEN { get; set; }

        public string CurrentLangName
        {
            get
            {
                var lang = LanguageHelper.GetCurrentLanguage();
                if (lang == Language.BG)
                    return this.ProductName;
                else if(lang == Language.EN)
                    return this.ProductNameEN;

                return this.ProductName;
            }
        }

        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public double Total
        {
            get
            {
                return ApplicationHelpers.RoundPrice(Price, Discount) * Quantity;
            }
        }

        public virtual ICollection<ProductImageW> ProductImages { get; set; }
    }
}