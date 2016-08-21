using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Полето \"Потребителско име\" е задължително!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Полето \"Парола\" е задължително!")]
        public string Password { get; set; }
    }
}