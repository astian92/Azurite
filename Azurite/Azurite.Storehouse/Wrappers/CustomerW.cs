using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class CustomerW : IMap, IMapFrom<Customer>
    {
        public CustomerW()
        {
            this.Orders = new HashSet<OrderViewModel>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Е-mail")]
        public string Email { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Display(Name = "Град")]
        public string City { get; set; }

        [Display(Name = "Държава")]
        public string Country { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Пощенски код")]
        public string ZipCode { get; set; }

        [Display(Name = "Фирма")]
        public string Company { get; set; }

        [Display(Name = "Булстат")]
        public string VatID { get; set; }

        public virtual ICollection<OrderViewModel> Orders { get; set; }
    }
}