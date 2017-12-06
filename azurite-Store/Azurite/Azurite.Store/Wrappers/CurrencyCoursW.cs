using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Store.Data;

namespace Azurite.Store.Wrappers
{
    public class CurrencyCoursW : IMap, IMapFrom<CurrencyCours>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public string Sign { get; set; }
    }
}