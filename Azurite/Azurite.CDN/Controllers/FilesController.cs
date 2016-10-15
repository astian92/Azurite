using Azurite.CDN.Data;
using Azurite.CDN.Workers.Contracts;
using Azurite.Infrastructure.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azurite.CDN.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFilesWorker worker;

        public FilesController(IFilesWorker worker)
        {
            this.worker = worker;
        }

        // GET: api/Files
        public async Task<HttpResponseMessage> Get(string filename)
        {
            var file = await worker.GetFile(filename);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(file.Bites);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            return result;
        }

        //ROUTE NOT WORKING !!!!!!
        [Route("{filename:guid}")]
        public async Task<HttpResponseMessage> Get(Guid filename)
        {
            var file = await worker.GetFileFromId(filename);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(file.Bites);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            return result;
        }

       
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Files/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Files
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Files/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Files/5
        public void Delete(int id)
        {
        }
    }
}
