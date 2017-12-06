using Azurite.CDN.Data;
using Azurite.CDN.Models.Http;
using Azurite.CDN.Services.Contracts;
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
    [System.Web.Mvc.SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class RemoveController : ApiController
    {
        private readonly IRemoveWorker worker;
        private readonly IKeyValidatorService keyValidator;

        public RemoveController(IRemoveWorker worker, IKeyValidatorService keyValidator)
        {
            this.worker = worker;
            this.keyValidator = keyValidator;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]MultipleIds multipleIds)
        {
            keyValidator.Validate(multipleIds.Key);

            worker.DeleteFiles(multipleIds.Ids);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
