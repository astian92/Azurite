using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.Http
{
    public class MultipleIds
    {
        public Guid Key { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
    }
}