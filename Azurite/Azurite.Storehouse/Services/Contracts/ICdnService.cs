using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azurite.Storehouse.Models.Http;

namespace Azurite.Storehouse.Services.Contracts
{
    public interface ICdnService
    {
        Task<bool> SaveFiles(IEnumerable<HttpFile> files);

        Task<bool> DeleteFiles(IEnumerable<Guid> fileIds);
    }
}