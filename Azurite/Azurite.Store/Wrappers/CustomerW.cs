using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Common;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class CustomerW : IMap, IMapFrom<Customer>
    {
        public Guid Id { get; set; }

        [Required]
        [LocalizedDisplayName("Email", NameResourceType = typeof(ViewRes.Customer))]
        public string Email { get; set; }

        [Required]
        [LocalizedDisplayName("FirstName", NameResourceType = typeof(ViewRes.Customer))]
        public string FirstName { get; set; }

        [Required]
        [LocalizedDisplayName("LastName", NameResourceType = typeof(ViewRes.Customer))]
        public string LastName { get; set; }

        [Required]
        [LocalizedDisplayName("Street", NameResourceType = typeof(ViewRes.Customer))]
        public string Street { get; set; }

        [Required]
        [LocalizedDisplayName("City", NameResourceType = typeof(ViewRes.Customer))]
        public string City { get; set; }

        [Required]
        [LocalizedDisplayName("Country", NameResourceType = typeof(ViewRes.Customer))]
        public string Country { get; set; }

        [Required]
        [LocalizedDisplayName("Phone", NameResourceType = typeof(ViewRes.Customer))]
        public string Phone { get; set; }

        [Required]
        [LocalizedDisplayName("ZipCode", NameResourceType = typeof(ViewRes.Customer))]
        public string ZipCode { get; set; }

        [LocalizedDisplayName("Company", NameResourceType = typeof(ViewRes.Customer))]
        public string Company { get; set; }

        [LocalizedDisplayName("VatID", NameResourceType = typeof(ViewRes.Customer))]
        public string VatID { get; set; }
    }
}