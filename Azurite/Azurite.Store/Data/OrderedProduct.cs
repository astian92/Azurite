//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Azurite.Store.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderedProduct
    {
        public System.Guid Id { get; set; }
        public System.Guid OrderId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
