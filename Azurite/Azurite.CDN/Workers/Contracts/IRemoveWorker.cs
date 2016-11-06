using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.CDN.Workers.Contracts
{
    public interface IRemoveWorker
    {
        void DeleteFiles(IEnumerable<Guid> ids);
    }
}