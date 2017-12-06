using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models
{
    public enum OrderStatuses
    {
        All = 0,
        Issued,
        InProcessing,
        Completed,
        Archived,
        Cancelled
    }
}