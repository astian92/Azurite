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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderedProducts = new HashSet<OrderedProduct>();
        }
    
        public System.Guid Id { get; set; }
        public string Number { get; set; }
        public System.Guid CustomerId { get; set; }
        public int StatusId { get; set; }
        public double Total { get; set; }
        public string Comment { get; set; }
        public System.DateTime Date { get; set; }
        public string Notes { get; set; }
        public int CurrencyId { get; set; }
    
        public virtual CurrencyCours CurrencyCours { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
