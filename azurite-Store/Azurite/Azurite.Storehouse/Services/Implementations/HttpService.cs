using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azurite.Storehouse.Services.Contracts;
using Newtonsoft.Json;

namespace Azurite.Storehouse.Services.Implementations
{
    public class HttpService : IHttpService
    {
        public async Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return await client.GetAsync(uri);
            }
        }

        public async Task<T> GetAsync<T>(Uri uri)
            where T : new()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var message = await client.GetAsync(uri);

                var contentStr = await message.Content.ReadAsStringAsync();
                T result = new T();
                result = JsonConvert.DeserializeAnonymousType<T>(contentStr, result);

                return result;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(Uri uri, object data)
        {
            using (var client = new HttpClient())
            {
                using (var request = BuildRequest("POST", uri))
                {
                    string requestContent = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                    using (var response = await client.SendAsync(request))
                    {
                        return response;
                    }
                }
            }
        }

        public async Task<T> PostAsync<T>(Uri uri, object data)
            where T: new()
        {
            using (var client = new HttpClient())
            {
                using (var request = BuildRequest("POST", uri))
                {
                    string requestContent = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                    using (var response = await client.SendAsync(request))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(content);

                        return result;
                    }
                }
            }
        }

        private HttpRequestMessage BuildRequest(string method, Uri uri)
        {
            var httpRequest = new HttpRequestMessage();
            httpRequest.Method = new HttpMethod(method);
            httpRequest.RequestUri = uri;

            return httpRequest;
        }
    }
}