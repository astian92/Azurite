using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Storehouse.Models.Http
{
    public class HttpFile
    {
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}