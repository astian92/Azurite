using Azurite.Store.Wrappers;
using System.Linq;

namespace Azurite.Store.Workers.Contracts
{
    public interface ICurrencyWorker
    {
        IQueryable<CurrencyCoursW> GetAll();
        CurrencyCoursW GetCurrent();
        void ChangeCurrent();
    }
}
