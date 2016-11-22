using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models
{
    public class OrdersCountList
    {
        public int All { get; set; }
        public int Issued { get; set; }
        public int InProcessing { get; set; }
        public int Completed { get; set; }
        public int Archived { get; set; }
        public int Cancelled { get; set; }
    }
}