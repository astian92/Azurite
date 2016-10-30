using Azurite.CDN.Data;
using Azurite.CDN.Models.Http;
using Azurite.CDN.Workers.Contracts;
using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azurite.CDN.Controllers
{
    public class RemoveController : ApiController
    {
        private readonly IRemoveWorker worker;

        public RemoveController(IRemoveWorker worker)
        {
            this.worker = worker;
        }

        public string Get()
        {
            return "delete controller";
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]SimpleId simpleId)
        {
            worker.DeleteFile(simpleId.Id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //[HttpPost]
        //public HttpResponseMessage Post([FromBody]MultipleIds multipleIds)
        //{
        //    worker.DeleteFiles(multipleIds.Ids);
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}
    }
}
