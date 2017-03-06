using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Configuration;
using Azurite.Storehouse.Models.Http;
using Azurite.Storehouse.Services.Contracts;

namespace Azurite.Storehouse.Services.Implementations
{
    public class CdnService : ICdnService
    {
        private readonly string _cdnAddress;
        private readonly IHttpService _httpService;
        private readonly Guid _key;

        public CdnService(IHttpService service)
        {
            this._httpService = service;
            _cdnAddress = WebConfigurationManager.AppSettings["CdnAddress"];
            string keyStr = WebConfigurationManager.AppSettings["SecurityKey"];
            this._key = Guid.Parse(keyStr);
        }

        public async Task<bool> SaveFiles(IEnumerable<HttpFile> files)
        {
            var result = await _httpService.PostAsync(new Uri(_cdnAddress + "save"), new { Key = this._key, files = files });
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(IEnumerable<Guid> fileIds)
        {
            var result = await _httpService.PostAsync(new Uri(_cdnAddress + "remove"), new MultipleIds() { Key = this._key, Ids = fileIds });
            return result.IsSuccessStatusCode;
        }
    }
}