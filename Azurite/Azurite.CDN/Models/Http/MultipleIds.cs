using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Models.Http
{
    public class MultipleIds
    {
        public Guid Key { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
    }
}