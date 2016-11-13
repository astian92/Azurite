using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Azurite.Store.Wrappers;
using AutoMapper;

namespace Azurite.Store.Workers.Implementations
{
    public class ProductWorker : IProductWorker
    {
        private readonly IRepository<Product> rep;
        private readonly IRepository<Category> catRep;

        public ProductWorker(IRepository<Product> rep, IRepository<Category> catRep)
        {
            this.rep = rep;
            this.catRep = catRep;
        }

        public IQueryable<ProductW> GetProducts(Guid categoryId)
        {
            var products = rep.GetAll();
            var wrapped = new List<ProductW>();

            foreach (var product in products.Where(x => x.Active && x.CategoryId == categoryId))
            {
                var mapped = Mapper.Map<ProductW>(product);
                wrapped.Add(mapped);
            }

            var category = catRep.Get(categoryId);
            var categoryW = Mapper.Map<CategoryW>(category);
            var subProducts = GetSubProducts(categoryW);
            if(subProducts != null)
                wrapped.AddRange(subProducts);

            return wrapped.AsQueryable();
        }

        private IQueryable<ProductW> GetSubProducts(CategoryW category)
        {
            var subCategories = GetSubCategories(category.Id);
            if (subCategories.Count() == 0)
                return null;

            var wrapped = new List<ProductW>();
            foreach (var subCategory in subCategories)
            {
                var products = rep.GetAll();
                foreach (var product in products.Where(x => x.Active && x.CategoryId == subCategory.Id))
                {
                    var mapped = Mapper.Map<ProductW>(product);
                    wrapped.Add(mapped);
                }

                var subProducts = GetSubProducts(subCategory);
                if(subProducts != null)
                    wrapped.AddRange(subProducts.AsQueryable());
            }

            return wrapped.AsQueryable();
        }

        public IQueryable<CategoryW> GetSubCategories(Guid categoryId)
        {
            var categories = catRep.GetAll();
            var wrapped = new List<CategoryW>();

            foreach (var cat in categories.Where(x => x.ParentId == categoryId))
            {
                var mapped = Mapper.Map<CategoryW>(cat);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public ProductW GetProduct(Guid productId)
        {
            var product = rep.Get(productId);
            var productW = Mapper.Map<ProductW>(product);
            return productW;
        }

        public IQueryable<ProductW> GetPromoProducts()
        {
            var products = rep.GetAll();

            var wrapped = new List<ProductW>();
            foreach (var product in products.Where(x => x.Active && x.Discount > 0).OrderBy(x => Guid.NewGuid()).Take(4))
            {
                var mapped = Mapper.Map<ProductW>(product);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }
    }
}