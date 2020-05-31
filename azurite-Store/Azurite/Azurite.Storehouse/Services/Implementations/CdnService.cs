﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Configuration;
using Azurite.Storehouse.Config.Constants;
using Azurite.Storehouse.Models.Http;
using Azurite.Storehouse.Services.Contracts;

namespace Azurite.Storehouse.Services.Implementations
{
    public class CdnService : ICdnService
    {
        //private readonly string cdnAddress;
        private readonly IHttpService httpService;
        private readonly Guid key;

        public CdnService(IHttpService service)
        {
            this.httpService = service;
            string keyStr = WebConfigurationManager.AppSettings["SecurityKey"];
            this.key = Guid.Parse(keyStr);
        }

        public async Task<bool> SaveFiles(IEnumerable<HttpFile> files)
        {
            var result = await httpService.PostAsync(new Uri(ServicePaths.Save), new { Key = this.key, files = files });
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFiles(IEnumerable<Guid> fileIds)
        {
            var result = await httpService.PostAsync(new Uri(ServicePaths.Remove), new MultipleIds() { Key = this.key, Ids = fileIds });
            return result.IsSuccessStatusCode;
        }
    }
}