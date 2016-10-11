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
using AutoMapper;
using Azurite.Infrastructure.ResponseHandling;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class ProductsWorker : IProductsWorker
    {
        private readonly IRepository<Product> rep;
        private readonly IRepository<Category> catRep;
        private readonly IRepository<CategoryAttribute> catAttrRep;
        private readonly IRepository<ProductAttribute> prodAttrRep;

        public ProductsWorker(IRepository<Product> rep, IRepository<Category> catRep, 
            IRepository<CategoryAttribute> catAttrRep, IRepository<ProductAttribute> prodAttrRep)
        {
            this.rep = rep;
            this.catRep = catRep;
            this.catAttrRep = catAttrRep;
            this.prodAttrRep = prodAttrRep;
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

        public ProductW Get(Guid productId)
        {
            var product = rep.Get(productId);
            var productW = Mapper.Map<ProductW>(product);

            var categoryAttributes = this.GetCategoryAttributes(product.CategoryId);

            foreach (var catAttr in categoryAttributes)
            {
                if (!productW.ProductAttributes.Any(pa => pa.AttributeId == catAttr.Id))
                {
                    var productAttribute = new ProductAttributeW();

                    productAttribute.AttributeId = catAttr.Id;
                    productAttribute.CategoryAttribute = Mapper.Map<CategoryAttributeSimpleW>(catAttr);

                    productW.ProductAttributes.Add(productAttribute);
                }
            }

            return productW;
        }

        public List<CategoryAttributeW> GetCategoryAttributes(Guid categoryId)
        {
            var category = catRep.Get(categoryId);

            var attributes = new List<CategoryAttributeW>();
            
            while(category != null)
            {
                foreach (var categoryAttribute in category.CategoryAttributes)
                {
                    var mapped = Mapper.Map<CategoryAttributeW>(categoryAttribute);
                    attributes.Add(mapped);
                }

                category = category.Category1;
            }
            
            return attributes;
        }
        
        public void Add(ProductW productW)
        {
            var product = Mapper.Map<Product>(productW);
            product.Id = Guid.NewGuid();

            foreach (var attribute in product.ProductAttributes)
            {
                attribute.Id = Guid.NewGuid();
            }

            rep.Add(product);
            rep.Save();
        }

        public void Edit(ProductW productW)
        {
            var product = rep.Get(productW.Id);

            //1 Update all properties
            product.CategoryId = productW.CategoryId;
            product.Number = productW.Number;
            product.Name = productW.Name;
            product.NameEN = productW.NameEN;
            product.Model = productW.Model;
            product.Description = productW.Description;
            product.DescriptionEN = productW.DescriptionEN;
            product.Price = productW.Price;
            product.Discount = productW.Discount;
            product.Quantity = productW.Quantity;
            product.Active = productW.Active;

            foreach (var attribute in productW.ProductAttributes)
            {
                if (product.ProductAttributes.Any(pa => pa.Id == attribute.Id))
                {
                    var existing = product.ProductAttributes.Single(pa => pa.Id == attribute.Id);
                    existing.Value = attribute.Value;
                    existing.ValueEN = attribute.ValueEN;
                }
                else
                {
                    var mapped = Mapper.Map<ProductAttribute>(attribute);
                    mapped.Id = Guid.NewGuid();
                    mapped.ProductId = product.Id;
                    product.ProductAttributes.Add(mapped);
                }
            }

            //then delete all that are not in new product
            var productAttributes = product.ProductAttributes.ToList();
            var oldAttributes = productAttributes.Where(pa => !productW.ProductAttributes.Any(pwa => pwa.AttributeId == pa.AttributeId));
            prodAttrRep.RemoveRange(oldAttributes);

            rep.Save();
        }

        public ITicket Delete(Guid Id)
        {
            try
            {
                rep.Remove(Id);
                rep.Save();
            }
            catch (DbUpdateException exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Опитвате се да изтриете запис, който се използва във връзка с други обекти!");
            }
            catch (SqlException exc)
            {
                ElmahHelper.Handle(exc);
                return new Ticket(false, "Възникна проблем при работа с базата!");
            }
            catch (Exception exc)
            {
                ElmahHelper.Handle(exc);
                string excName = exc.GetType().Name;
                return new Ticket(false, "Възникна неочаквана грешка!");
            }

            return new Ticket(true);
        }
    }
}