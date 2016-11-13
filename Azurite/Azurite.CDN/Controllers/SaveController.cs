using Azurite.CDN.Models;
using Azurite.CDN.Models.Http;
using Azurite.CDN.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azurite.CDN.Controllers
{
    [System.Web.Mvc.SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class SaveController : ApiController
    {
        private readonly ISaveWorker worker;

        public SaveController(ISaveWorker worker)
        {
            this.worker = worker;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(ProductFiles data)
        {
            worker.SaveFiles(data.Files);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            return result;
        }
    }
}
