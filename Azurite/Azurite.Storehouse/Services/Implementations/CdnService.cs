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
        private readonly string cdnAddress;
        private readonly IHttpService httpService;
        private readonly Guid key;

        public CdnService(IHttpService service)
        {
            this.httpService = service;
            cdnAddress = WebConfigurationManager.AppSettings["CdnAddress"];
            string keyStr = WebConfigurationManager.AppSettings["SecurityKey"];
            this.key = Guid.Parse(keyStr);
        }

        public async Task<bool> SaveFiles(IEnumerable<HttpFile> files)
        {
            var result = await httpService.PostAsync(new Uri(cdnAddress + "save"), new { Key = this.key, files = files });
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(IEnumerable<Guid> fileIds)
        {
            var result = await httpService.PostAsync(new Uri(cdnAddress + "remove"), new MultipleIds() { Key = this.key, Ids = fileIds });
            return result.IsSuccessStatusCode;
        }
    }
}