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
            //var content = ConvertToStringForm(data);
            var dataString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept
                   .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return await client.PostAsync(uri, content);
            }
        }

        public async Task<T> PostAsync<T>(Uri uri, object data)
            where T: new()
        {
            //var content = ConvertToStringForm(data);
            var dataString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var message = await client.PostAsync(uri, content);

                var contentStr = await message.Content.ReadAsStringAsync();
                T result = new T();
                result = JsonConvert.DeserializeAnonymousType<T>(contentStr, result);

                return result;
            }
        }

        ////Converts the object that is send as data in the http request to a FormUrlEncoded content
        ////so it can be easily send with the request
        //private FormUrlEncodedContent ConvertToStringForm(object obj)
        //{
        //    var type = obj.GetType();
        //    var props = type.GetProperties();

        //    var list = new List<KeyValuePair<string, string>>();

        //    foreach (var prop in props)
        //    {
        //        var pair = new KeyValuePair<string, string>(prop.Name, prop.GetValue(obj).ToString());
        //        list.Add(pair);
        //    }

        //    return new FormUrlEncodedContent(list);
        //}
    }
}