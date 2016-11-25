using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Common;
using Azurite.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Wrappers
{
    public class CustomerW : IMap, IMapFrom<Customer>
    {
        public Guid Id { get; set; }

        [LocalizedDisplayName("Email", NameResourceType = typeof(Customer))]
        public string Email { get; set; }

        [LocalizedDisplayName("FirstName", NameResourceType = typeof(Customer))]
        public string FirstName { get; set; }

        [LocalizedDisplayName("LastName", NameResourceType = typeof(Customer))]
        public string LastName { get; set; }

        [LocalizedDisplayName("Street", NameResourceType = typeof(Customer))]
        public string Street { get; set; }

        [LocalizedDisplayName("City", NameResourceType = typeof(Customer))]
        public string City { get; set; }

        [LocalizedDisplayName("Country", NameResourceType = typeof(Customer))]
        public string Country { get; set; }

        [LocalizedDisplayName("Phone", NameResourceType = typeof(Customer))]
        public string Phone { get; set; }

        [LocalizedDisplayName("ZipCode", NameResourceType = typeof(Customer))]
        public string ZipCode { get; set; }

        [LocalizedDisplayName("Company", NameResourceType = typeof(Customer))]
        public string Company { get; set; }

        [LocalizedDisplayName("VatID", NameResourceType = typeof(Customer))]
        public string VatID { get; set; }
    }
}