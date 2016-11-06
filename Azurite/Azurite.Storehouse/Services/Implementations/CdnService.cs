using Azurite.Storehouse.Models.Http;
using Azurite.Storehouse.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Azurite.Storehouse.Services.Implementations
{
    public class CdnService : ICdnService
    {
        private readonly string CdnAddress;
        private readonly IHttpService httpService;

        public CdnService(IHttpService service)
        {
            this.httpService = service;
            CdnAddress = WebConfigurationManager.AppSettings["CdnAddress"];
        }

        public async Task<bool> SaveFiles(IEnumerable<HttpFile> files)
        {
            var result = await httpService.PostAsync(new Uri(CdnAddress + "save"), new { files = files });
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(IEnumerable<Guid> fileIds)
        {
            var result = await httpService.PostAsync(new Uri(CdnAddress + "remove"), new MultipleIds() { Ids = fileIds });
            return result.IsSuccessStatusCode;
        }
    }
}