using System;
using System.Collections.Generic;
using Azurite.CDN.Models.Http;

namespace Azurite.CDN.Models
{
    public class ProductFiles
    {
        public Guid Key { get; set; }
        public IEnumerable<HttpFile> Files { get; set; }
    }
}