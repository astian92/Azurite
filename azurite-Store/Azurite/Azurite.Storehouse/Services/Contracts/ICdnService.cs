using Azurite.Storehouse.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Azurite.Storehouse.Services.Contracts
{
    public interface ICdnService
    {
        Task<bool> SaveFiles(IEnumerable<HttpFile> files);
        Task<bool> DeleteFiles(IEnumerable<Guid> fileIds);
    }
}