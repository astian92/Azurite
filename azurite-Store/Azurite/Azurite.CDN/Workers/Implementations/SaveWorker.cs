using Azurite.CDN.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.CDN.Models.Http;
using System.Web.Hosting;
using System.IO;

namespace Azurite.CDN.Workers.Implementations
{
    public class SaveWorker : ISaveWorker
    {
        private readonly string ContentPath = HostingEnvironment.MapPath(@"~/Content");

        public void SaveFiles(IEnumerable<HttpFile> files)
        {
            foreach (var file in files)
            {
                File.WriteAllBytes(ContentPath + @"/" + file.Filename, file.Content);
            }
        }
    }
}