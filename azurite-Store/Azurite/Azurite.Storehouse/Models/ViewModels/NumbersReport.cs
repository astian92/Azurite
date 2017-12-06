using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.ViewModels
{
    public class NumbersReport
    {
        public decimal Number { get; set; }

        public List<decimal> Report { get; set; }

        public NumbersReport()
        {
            this.Report = new List<decimal>();
        }
    }
}