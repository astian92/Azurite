//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Azurite.Storehouse.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductAttribute
    {
        public System.Guid Id { get; set; }
        public System.Guid AttributeId { get; set; }
        public System.Guid ProductId { get; set; }
        public string Value { get; set; }
        public string ValueEN { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual CategoryAttribute CategoryAttribute { get; set; }
    }
}
