using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Store.Wrappers;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using AutoMapper;

namespace Azurite.Store.Workers.Implementations
{
    public class CurrencyWorker : ICurrencyWorker
    {
        private readonly IRepository<CurrencyCours> rep;

        public CurrencyWorker(IRepository<CurrencyCours> rep)
        {
            this.rep = rep;
        }

        public IQueryable<CurrencyCoursW> GetAll()
        {
            var currencies = rep.GetAll();

            var wrapped = new List<CurrencyCoursW>();
            foreach (var currency in currencies)
            {
                var mapped = Mapper.Map<CurrencyCoursW>(currency);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public void ChangeCurrent()
        {
            throw new NotImplementedException();
        }

        public CurrencyCoursW GetCurrent()
        {
            throw new NotImplementedException();
        }
    }
}