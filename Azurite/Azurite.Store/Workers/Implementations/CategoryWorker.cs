using AutoMapper;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Store.Data;
using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azurite.Store.Workers.Implementations
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

        public IQueryable<CategoryW> GetBaseCategories()
        {
            var categories = rep.GetAll();
            var wrapped = new List<CategoryW>();

            foreach (var cat in categories.Where(x => x.ParentId == null))
            {
                var mapped = Mapper.Map<CategoryW>(cat);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public IQueryable<CategoryW> GetSubCategories(Guid categoryId)
        {
            var categories = rep.GetAll();
            var wrapped = new List<CategoryW>();

            foreach (var cat  in categories.Where(x => x.ParentId == categoryId))
            {
                var mapped = Mapper.Map<CategoryW>(cat);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public CategoryW GetCategory(Guid categoryId)
        {
            var category = rep.Get(categoryId);
            var categoryW = Mapper.Map<CategoryW>(category);

            return categoryW;
        }

        public IQueryable<CategoryAttributeW> GetCategoryAttr(Guid categoryId)
        {
            var category = rep.Get(categoryId);
            var wrapped = new List<CategoryAttributeW>();

            foreach (var attr in category.CategoryAttributes)
            {
                var mapped = Mapper.Map<CategoryAttributeW>(attr);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }
    }
}