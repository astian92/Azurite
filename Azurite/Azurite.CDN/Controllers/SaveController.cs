using Azurite.CDN.Models.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Azurite.CDN.Controllers
{
    public class SaveController : ApiController
    {
        public async Task<HttpResponseMessage> Post(HttpFile file)
        {
            //Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent("asd");
            result.Content.Headers.Add("Access-Control-Allow-Origin", "*");

            return result;
        }

        public async Task<HttpResponseMessage> Post(IEnumerable<HttpFile> files)
        {
            return null;
        }

        public string Get()
        {
            return "save controller";
        }
    }
}
