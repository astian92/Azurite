using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Wrappers;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Models.Helpers;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CurrencyWorker : ICurrencyWorker
    {
        private readonly IRepository<CurrencyCours> _rep;

        public CurrencyWorker(IRepository<CurrencyCours> rep)
        {
            this._rep = rep;
        }

        public CurrencyCoursW Get(int Id)
        {
            var currency = _rep.Get(Id);
            var currencyW = Mapper.Map<CurrencyCoursW>(currency);

            return currencyW;
        }

        public IQueryable<CurrencyCoursW> GetAll()
        {
            return _rep.GetAll()
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
                var currency = _rep.Get(currencyW.Id);
                currency.Code = currencyW.Code;
                currency.Sign = currencyW.Sign;
                currency.Value = currencyW.Value;

                _rep.Save();

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