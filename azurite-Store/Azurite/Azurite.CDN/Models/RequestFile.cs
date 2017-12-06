using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Models
{
    public class RequestFile
    {
        public byte[] Bites { get; set; }
        public string ContentType { get; set; }
    }
}