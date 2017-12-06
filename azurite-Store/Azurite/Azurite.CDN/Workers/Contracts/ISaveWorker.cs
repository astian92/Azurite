using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.CDN.Models.Http;

namespace Azurite.CDN.Workers.Contracts
{
    public interface ISaveWorker
    {
        void SaveFiles(IEnumerable<HttpFile> files);
    }
}