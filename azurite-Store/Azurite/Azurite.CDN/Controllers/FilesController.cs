using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Azurite.CDN.Attributes;
using Azurite.CDN.Workers.Contracts;

namespace Azurite.CDN.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class FilesController : ApiController
    {
        private readonly IFilesWorker worker;

        public FilesController(IFilesWorker worker)
        {
            this.worker = worker;
        }

        [DisableResponseLogging]
        public async Task<HttpResponseMessage> Get(Guid productId)
        {
            var file = await worker.GetFileFromId(productId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(file.Bites);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            return result;
        }

        // GET: api/Files
        [DisableResponseLogging]
        public async Task<HttpResponseMessage> Get(string filename)
        {
            var file = await worker.GetFile(filename);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(file.Bites);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            return result;
        }
    }
}
