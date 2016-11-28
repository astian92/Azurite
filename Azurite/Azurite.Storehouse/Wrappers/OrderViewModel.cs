using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderViewModel : IMap, IMapFrom<Order>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid CustomerId { get; set; }
        public int StatusId { get; set; }
        public double Total { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public virtual OrderStatusW OrderStatus { get; set; }

        public string DateStr
        {
            get
            {
                return this.Date.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                //.ForMember(d => d.Date, conf => conf.MapFrom(s => s.Date.ToLocalTime()))
                .ForMember(d => d.CustomerName, conf => conf.MapFrom(s => s.Customer.FirstName + " " + s.Customer.LastName));
        }
    }
}