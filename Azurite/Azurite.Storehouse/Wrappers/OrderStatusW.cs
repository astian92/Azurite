using System.ComponentModel.DataAnnotations;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers
{
    public class OrderStatusW : IMap, IMapFrom<OrderStatus>
    {
        public int Id { get; set; }

        [Display(Name = "Състояние")]
        public string DisplayName { get; set; }

        public string DisplayNameEN { get; set; }
    }
}