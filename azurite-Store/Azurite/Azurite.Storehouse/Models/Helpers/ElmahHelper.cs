using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.Helpers
{
    public static class ElmahHelper
    {
        public static void Handle(Exception exc)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exc);
        }
    }
}