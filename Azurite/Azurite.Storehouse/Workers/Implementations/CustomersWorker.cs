using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CustomersWorker : ICustomersWorker
    {
        private readonly IRepository<Customer> _rep;

        public CustomersWorker(IRepository<Customer> rep)
        {
            this._rep = rep;
        }

        public CustomerW Get(Guid Id)
        {
            var customer = _rep.Get(Id);
            var custW = Mapper.Map<CustomerW>(customer);

            return custW;
        }

        public IQueryable<CustomerIndexViewModel> GetAll()
        {
            return _rep.GetAll()
                .ProjectTo<CustomerIndexViewModel>();
        }
    }
}