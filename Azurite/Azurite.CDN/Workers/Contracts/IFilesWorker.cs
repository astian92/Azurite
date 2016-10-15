using Azurite.CDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Azurite.CDN.Workers.Contracts
{
    public interface IFilesWorker
    {
        Task<RequestFile> GetFile(string filepath);
        Task<RequestFile> GetFileFromId(Guid fileId);
    }
}