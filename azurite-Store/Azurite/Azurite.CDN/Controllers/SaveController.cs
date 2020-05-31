using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Azurite.CDN.Filters;
using Azurite.CDN.Models;
using Azurite.CDN.Services.Contracts;
using Azurite.CDN.Workers.Contracts;
using log4net;

namespace Azurite.CDN.Controllers
{
    [System.Web.Mvc.SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    [GlobalExceptionFilter]
    public class SaveController : ApiController
    {
        private readonly ISaveWorker worker;
        private readonly IKeyValidatorService keyValidator;
        private readonly ILog logger;

        public SaveController(ISaveWorker worker, IKeyValidatorService keyValidator, ILog logger)
        {
            this.worker = worker;
            this.keyValidator = keyValidator;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(ProductFiles data)
        {
            logger.Info($"Entered saving files. Files to save: {data.Files.Count()}");
            keyValidator.Validate(data.Key);

            try
            {
                worker.SaveFiles(data.Files);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                return result;
            }
            catch (Exception exc)
            {
                logger.Error("There was a problem saving file.", exc);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
        }
    }
}
