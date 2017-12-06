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

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "EmailValidation", ErrorMessage = null)]
        [LocalizedDisplayName("Email", NameResourceType = typeof(ViewRes.Customer))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "FirstNameValidation", ErrorMessage = null)]
        [LocalizedDisplayName("FirstName", NameResourceType = typeof(ViewRes.Customer))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "LastNameValidation", ErrorMessage = null)]
        [LocalizedDisplayName("LastName", NameResourceType = typeof(ViewRes.Customer))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "StreetValidation", ErrorMessage = null)]
        [LocalizedDisplayName("Street", NameResourceType = typeof(ViewRes.Customer))]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "CityValidation", ErrorMessage = null)]
        [LocalizedDisplayName("City", NameResourceType = typeof(ViewRes.Customer))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "CountryValidation", ErrorMessage = null)]
        [LocalizedDisplayName("Country", NameResourceType = typeof(ViewRes.Customer))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "PhoneValidation", ErrorMessage = null)]
        [LocalizedDisplayName("Phone", NameResourceType = typeof(ViewRes.Customer))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewRes.Customer), ErrorMessageResourceName = "ZipCodeValidation", ErrorMessage = null)]
        [LocalizedDisplayName("ZipCode", NameResourceType = typeof(ViewRes.Customer))]
        public string ZipCode { get; set; }

        [LocalizedDisplayName("Company", NameResourceType = typeof(ViewRes.Customer))]
        public string Company { get; set; }

        [LocalizedDisplayName("VatID", NameResourceType = typeof(ViewRes.Customer))]
        public string VatID { get; set; }
    }
}