using Azurite.CDN.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Models
{
    public class ProductFiles
    {
        public Guid Key { get; set; }
        public IEnumerable<HttpFile> Files { get; set; }
    }
}