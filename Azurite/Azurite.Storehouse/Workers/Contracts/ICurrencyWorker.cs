using System.Linq;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Workers.Contracts
{
    public interface ICurrencyWorker
    {
        CurrencyCoursW Get(int Id);

        IQueryable<CurrencyCoursW> GetAll();

        ITicket Add(CurrencyCoursW currencyW);

        ITicket Edit(CurrencyCoursW currencyW);

        ITicket Delete(int Id);
    }
}