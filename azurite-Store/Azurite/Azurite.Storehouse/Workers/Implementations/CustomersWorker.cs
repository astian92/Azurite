using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Wrappers;
using Azurite.Infrastructure.Data.Contracts;
using AutoMapper.QueryableExtensions;
using Azurite.Storehouse.Data;
using AutoMapper;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CustomersWorker : ICustomersWorker
    {
        private readonly IRepository<Customer> rep;

        public CustomersWorker(IRepository<Customer> rep)
        {
            this.rep = rep;
        }

        public CustomerW Get(Guid Id)
        {
            var customer = rep.Get(Id);
            var custW = Mapper.Map<CustomerW>(customer);

            return custW;
        }

        public IQueryable<CustomerIndexViewModel> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<CustomerIndexViewModel>();
        }


    }
}