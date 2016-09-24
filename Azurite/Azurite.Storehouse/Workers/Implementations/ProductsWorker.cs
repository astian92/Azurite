using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Storehouse.Wrappers;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using AutoMapper.QueryableExtensions;
using Azurite.Storehouse.Models.Helpers;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class ProductsWorker : IProductsWorker
    {
        private readonly IRepository<Product> rep;
        private readonly IRepository<Category> catRep;

        public ProductsWorker(IRepository<Product> rep, IRepository<Category> catRep)
        {
            this.rep = rep;
            this.catRep = catRep;
        }

        public IQueryable<ProductIndexViewModel> GetAll()
        {
            return rep.GetAll()
                .ProjectTo<ProductIndexViewModel>();
        }

        public List<DropDownItem> GetCategoriesDropDownItems()
        {
            var items = catRep.GetAll()
                .Select(c => new { Name = c.Name, Id = c.Id }) //to project only columns we want so it is a lighter query
                .ToList() //to enumerate
                .Select(c => new DropDownItem(c.Id.ToString(), c.Name))
                .ToList();

            return items;
        }
    }
}