using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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