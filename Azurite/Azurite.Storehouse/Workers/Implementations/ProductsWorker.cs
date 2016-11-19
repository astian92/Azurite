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
using Azurite.Storehouse.Services.Contracts;
using System.Threading.Tasks;
using Azurite.Storehouse.Config.Constants;
using Azurite.Storehouse.Models.Http;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class ProductsWorker : IProductsWorker
    {
        private readonly IRepository<Product> rep;
        private readonly IRepository<Category> catRep;
        private readonly IRepository<CategoryAttribute> catAttrRep;
        private readonly IRepository<ProductAttribute> prodAttrRep;
        private readonly IRepository<ProductImage> prodImgRep;
        private readonly ICdnService cdnService;

        public ProductsWorker(IRepository<Product> rep, IRepository<Category> catRep,
            IRepository<CategoryAttribute> catAttrRep, IRepository<ProductAttribute> prodAttrRep,
             IRepository<ProductImage> prodImgRep, ICdnService service)
        {
            this.rep = rep;
            this.catRep = catRep;
            this.catAttrRep = catAttrRep;
            this.prodAttrRep = prodAttrRep;
            this.prodImgRep = prodImgRep;
            this.cdnService = service;
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

            while (category != null)
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

        public async Task<ITicket> Add(ProductW productW, IEnumerable<HttpPostedFileBase> photos)
        {
            var product = Mapper.Map<Product>(productW);
            product.Id = Guid.NewGuid();

            foreach (var attribute in product.ProductAttributes)
            {
                attribute.Id = Guid.NewGuid();
            }

            ITicket ticket = null;
            List<HttpFile> files = new List<HttpFile>();
            foreach (var photo in photos.Where(p => p != null))
            {
                var productImage = new ProductImage();
                productImage.Id = Guid.NewGuid();
                productImage.ImagePath = ImportantVariables.ProductsPrefix + photo.FileName;

                product.ProductImages.Add(productImage);

                HttpFile file = new HttpFile();
                file.Filename = productImage.ImagePath;
                file.Content = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(file.Content, 0, photo.ContentLength);

                files.Add(file);
            }

            //then send them to the service:
            if (files.Count > 0)
            {
                bool success = await cdnService.SaveFiles(files);

                if (success == false)
                {
                    ticket = new Ticket(false, "Записването на изображения се провали!");
                }
            }

            rep.Add(product);
            rep.Save();

            if (ticket == null)
            {
                ticket = new Ticket(true);
            }

            return ticket;
        }

        public async Task<ITicket> Edit(ProductW productW, IEnumerable<HttpPostedFileBase> photos, IEnumerable<Guid> imageIds)
        {
            try
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

                //now handle files ..
                ITicket ticket = null;
                //first get the ids of the files that remain and delete those that are no longer in the list
                if (imageIds == null) //when there are NONE left we need an empty list
                {
                    imageIds = new List<Guid>();
                }
                var removedFiles = product.ProductImages.Where(i => !imageIds.Any(imd => imd == i.Id)).ToList();
                prodImgRep.RemoveRange(removedFiles);

                //if there were files that were removed then send their ids to the CDN for removal
                if (removedFiles.Count() > 0)
                {
                    bool success = await cdnService.DeleteFiles(removedFiles.Select(f => f.Id));

                    if (success == false)
                    {
                        ticket = new Ticket(false, "Изтриването на изображения се провали!");
                    }
                }

                //now, lets save the new files. First we need to prepare them
                List<HttpFile> files = new List<HttpFile>();
                foreach (var photo in photos.Where(p => p != null))
                {
                    var productImage = new ProductImage();
                    productImage.Id = Guid.NewGuid();
                    productImage.ImagePath = ImportantVariables.ProductsPrefix + photo.FileName;

                    product.ProductImages.Add(productImage);

                    HttpFile file = new HttpFile();
                    file.Filename = productImage.ImagePath;
                    file.Content = new byte[photo.ContentLength];
                    await photo.InputStream.ReadAsync(file.Content, 0, photo.ContentLength);

                    files.Add(file);
                }

                //then send them to the service:
                if (files.Count > 0)
                {
                    bool success = await cdnService.SaveFiles(files);

                    if (success == false)
                    {
                        ticket = new Ticket(false, "Записването на изображения се провали!");
                    }
                }

                rep.Save();

                if (ticket == null)
                {
                    ticket = new Ticket(true);
                }

                return ticket;
            }
            catch (DbUpdateException sqlExc)
            {
                ElmahHelper.Handle(sqlExc);
                return new Ticket(false, "Има продукти с данни за изтрития атрибут(и)");
            }
            catch (Exception e)
            {
                string type = e.GetType().Name;
                ElmahHelper.Handle(e);
                return new Ticket(false, "Възникна неочаквана грешка!");
            }
        }

        public async Task<ITicket> Delete(Guid Id)
        {
            try
            {
                var product = rep.Get(Id);
                var imageIds = product.ProductImages.Select(p => p.Id).ToList();
                prodImgRep.RemoveRange(product.ProductImages);

                //send images for deletion
                var result = await cdnService.DeleteFiles(imageIds);

                if (result == false)
                {
                    //silent fail
                    var exc = new Exception("The images for product: product: " + product.Number + "were not deleted correctly!");
                    ElmahHelper.Handle(exc);
                }

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