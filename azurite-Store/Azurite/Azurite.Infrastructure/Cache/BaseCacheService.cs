using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Azurite.Infrastructure.Cache
{
    public abstract class BaseCacheService
    {
        protected T Get<T>(string cacheKey, Func<T> getItemCallback)
            where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheKey, item);
                return item;
            }
            return item;
        }
    }
}
