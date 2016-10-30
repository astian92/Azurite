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
        Task<bool> SaveFile(Guid productId, HttpFile file);
        Task<bool> SaveFiles(Guid productId, IEnumerable<HttpFile> files);
        Task<bool> DeleteFile(Guid fileId);
        Task<bool> DeleteFiles(IEnumerable<Guid> fileIds);
    }
}