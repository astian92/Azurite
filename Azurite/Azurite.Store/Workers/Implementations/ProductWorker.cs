using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Store.Wrappers;

namespace Azurite.Store.Workers.Implementations
{
    public class ProductWorker : IProductWorker
    {
        private readonly IRepository<Product> rep;

        public ProductWorker(IRepository<Product> rep)
        {
            this.rep = rep;
        }

        public IQueryable<ProductW> GetProducts(Guid categoryId)
        {
            throw new NotImplementedException();
        }
    }
}