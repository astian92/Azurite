using Azurite.Storehouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface IAccountWorker
    {
        bool Authenticate(LoginViewModel model);
    }
}