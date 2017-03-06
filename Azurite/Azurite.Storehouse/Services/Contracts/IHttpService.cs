using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Azurite.Storehouse.Services.Contracts
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(Uri uri);

        Task<T> GetAsync<T>(Uri uri) where T : new();

        Task<HttpResponseMessage> PostAsync(Uri uri, object data);

        Task<T> PostAsync<T>(Uri uri, object data) where T : new();
    }
}