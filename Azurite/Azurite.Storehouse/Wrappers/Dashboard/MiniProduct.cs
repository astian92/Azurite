using Azurite.Infrastructure.Mapping.Contracts;
using Azurite.Storehouse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Wrappers.Dashboard
{
    public class MiniProduct : IMap, IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
    }
}