using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Azurite.Storehouse.Models.Helpers;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CurrencyWorker : ICurrencyWorker
    {
        private readonly IRepository<CurrencyCours> rep;

        public CurrencyWorker(IRepository<CurrencyCours> rep)
        {
            this.rep = rep;
        }

        public CurrencyCoursW Get(int Id)
        {
            var currency = rep.Get(Id);
            var currencyW = Mapper.Map<CurrencyCoursW>(currency);

            return currencyW;
        }

        public IQueryable<CurrencyCoursW> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<CurrencyCoursW>();
        }

        public ITicket Add(CurrencyCoursW currencyW)
        {
            throw new NotImplementedException();
        }

        public ITicket Edit(CurrencyCoursW currencyW)
        {
            try
            {
                var currency = rep.Get(currencyW.Id);
                currency.Code = currencyW.Code;
                currency.Sign = currencyW.Sign;
                currency.Value = currencyW.Value;

                rep.Save();

                return new Ticket(true);
            }
            catch (Exception exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Възникна проблем с промяната на валута!");
            }
        }

        public ITicket Delete(int Id)
        {
            throw new NotImplementedException();
        }

    }
}