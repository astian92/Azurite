using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderW : IMap, IMapFrom<Order>, IHaveCustomMappings
    {
        public OrderW()
        {
            this.OrderedProducts = new HashSet<OrderedProductW>();
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public int StatusId { get; set; }

        [Display(Name = "Номер")]
        public string Number { get; set; }

        [Display(Name = "Сума")]
        public double Total { get; set; }

        [Display(Name = "Коментар")]
        public string Comment { get; set; }

        [Display(Name = "Дата и час")]
        public DateTime Date { get; set; }

        [Display(Name = "Бележки")]
        public string Notes { get; set; }

        public string DateStr
        {
            get
            {
                return this.Date.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
            }
        }

        public virtual CustomerW Customer { get; set; }

        public virtual ICollection<OrderedProductW> OrderedProducts { get; set; }

        public virtual OrderStatusW OrderStatus { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Order, OrderW>()
                .ReverseMap()
                .ForMember(d => d.Date, conf => conf.MapFrom(s => s.Date.ToUniversalTime()));
        }
    }
}