using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Azurite.CDN.Filters;
using Azurite.CDN.Models.Http;
using Azurite.CDN.Services.Contracts;
using Azurite.CDN.Workers.Contracts;
using log4net;

namespace Azurite.CDN.Controllers
{
    [System.Web.Mvc.SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    [GlobalExceptionFilter]
    public class RemoveController : ApiController
    {
        private readonly IRemoveWorker worker;
        private readonly IKeyValidatorService keyValidator;
        private readonly ILog logger;

        public RemoveController(IRemoveWorker worker, IKeyValidatorService keyValidator, ILog logger)
        {
            this.worker = worker;
            this.keyValidator = keyValidator;
            this.logger = logger;
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
