using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Azurite.Storehouse.Wrappers.Dashboard
{
    public class MiniOrder : IMap, IMapFrom<Order>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }

        public string DateStr
        {
            get
            {
                return this.Date.ToLocalTime().ToString("dd-MM-yyyy");
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Order, MiniOrder>()
                .ForMember(d => d.CustomerName, conf => conf.MapFrom(s => s.Customer.FirstName + " " + s.Customer.LastName));
        }
    }
}