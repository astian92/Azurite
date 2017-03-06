using System;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers.Dashboard
{
    public class MiniOrder : IMap, IMapFrom<Order>
    {
        public Guid Id { get; set; }

        public double Total { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string DateStr
        {
            get
            {
                return this.Date.ToLocalTime().ToString("dd-MM-yyyy");
            }
        }
    }
}