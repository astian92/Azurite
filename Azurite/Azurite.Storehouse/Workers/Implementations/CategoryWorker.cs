using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Wrappers;
using AutoMapper.QueryableExtensions;
using AutoMapper;

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
            var categories = rep.GetAll();
            List<CategoryW> wrapped = new List<CategoryW>();

            //because I cant escape circular reference and ProjectTo does not work with max depth
            foreach (var cat in categories)
            {
                var mapped = Mapper.Map<CategoryW>(cat);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public void Add(CategoryW categoryW)
        {
            var category = Mapper.Map<Category>(categoryW);
            category.Id = Guid.NewGuid();
            rep.Add(category);

            rep.Save();
        }
    }
}