using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Azurite.CDN.Models.Http;
using Azurite.CDN.Workers.Contracts;

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