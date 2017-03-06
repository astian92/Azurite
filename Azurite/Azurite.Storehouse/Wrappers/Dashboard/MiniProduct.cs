using System;
using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Wrappers.Dashboard
{
    public class MiniProduct : IMap, IMapFrom<Product>
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public string Name { get; set; }
    }
}