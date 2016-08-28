using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Wrappers;
using AutoMapper.QueryableExtensions;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CategoryWorker : ICategoryWorker
    {
        private readonly IRepository<Category> rep;

        public CategoryWorker(IRepository<Category> rep)
        {
            this.rep = rep;
        }

        public IQueryable<CategoryW> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<CategoryW>();
        }
    }
}