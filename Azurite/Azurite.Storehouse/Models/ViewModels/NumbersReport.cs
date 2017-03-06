using System.Collections.Generic;

namespace Azurite.Storehouse.Models.ViewModels
{
    public class NumbersReport
    {
        public NumbersReport()
        {
            this.Report = new List<decimal>();
        }

        public decimal Number { get; set; }

        public List<decimal> Report { get; set; }
    }
}