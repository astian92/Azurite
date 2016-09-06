using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers
{
    public class UserW : IMap, IMapFrom<User>
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Полето \"Потребителско име\" е задължително!")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Полето \"Парола\" е задължително!")]
        [Display(Name = "Парола")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Полето \"Фамилия\" е задължително!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}