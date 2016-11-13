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
using Azurite.Infrastructure.Data.Implementations;
using Azurite.Storehouse.Models.Infrastructure;
using System.Data.Entity;
using Azurite.Infrastructure.ResponseHandling;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Services.Contracts;
using System.Threading.Tasks;
using Azurite.Storehouse.Config.Constants;
using Azurite.Storehouse.Models.Http;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CategoriesWorker : ICategoriesWorker
    {
        private readonly IRepository<Category> rep;
        private readonly IRepository<CategoryAttribute> attrRep;
        private readonly ICdnService cdnService;

        public CategoriesWorker(IRepository<Category> rep, IRepository<CategoryAttribute> attrRep, ICdnService cdnService)
        {
            this.rep = rep;
            this.attrRep = attrRep;
            this.cdnService = cdnService;
        }

        public CategoryW Get(Guid Id)
        {
            var cat = rep.Get(Id);
            var catW = Mapper.Map<CategoryW>(cat);

            return catW;
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

        public IQueryable<CategoryIndexViewModel> GetAllWithoutParents()
        {
            var categories = rep.GetAll();
            List<CategoryIndexViewModel> wrapped = new List<CategoryIndexViewModel>();

            //this time projectTo breaks on testing! 
            foreach (var cat in categories)
            {
                var mapped = Mapper.Map<CategoryIndexViewModel>(cat);
                wrapped.Add(mapped);
            }

            return wrapped.AsQueryable();
        }

        public async Task<ITicket> Add(CategoryW categoryW, HttpPostedFileBase photo)
        {
            var category = Mapper.Map<Category>(categoryW);
            category.Id = Guid.NewGuid();

            if (category.ParentId == Guid.Empty)
            {
                category.ParentId = null;
            }

            ITicket ticket = null;
            if (photo != null)
            {
                category.ImagePath = ImportantVariables.CategoriesPrefix + photo.FileName;

                List<HttpFile> files = new List<HttpFile>();
                HttpFile file = new HttpFile();
                file.Filename = category.ImagePath;
                file.Content = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(file.Content, 0, photo.ContentLength);

                files.Add(file);

                if (files.Count > 0)
                {
                    bool success = await cdnService.SaveFiles(files);

                    if (success == false)
                    {
                        ticket = new Ticket(false, "Записването на изображения се провали!");
                    }
                }
            }

            rep.Add(category);
            rep.Save();

            if (ticket == null)
            {
                ticket = new Ticket(true);
            }

            return ticket;
        }

        public async Task<ITicket> Edit(CategoryW categoryW, HttpPostedFileBase photo, bool deleted)
        {
            var category = rep.Get(categoryW.Id);
            
            //1 update all properties
            category.Name = categoryW.Name;
            category.NameEN = categoryW.NameEN;
            category.Description = categoryW.Description;
            category.DescriptionEN = categoryW.DescriptionEN;
            if (categoryW.ParentId != null && categoryW.ParentId != Guid.Empty)
            {
                category.ParentId = categoryW.ParentId;
            }
            else
            {
                category.ParentId = null;
            }

            //attrRep.Save();
            //2.remove deleted items
            var removed = category.CategoryAttributes.Where(ca => !categoryW.CategoryAttributes.Any(caw => caw.Id == ca.Id));
            attrRep.RemoveRange(removed);

            //3. Update isActive on those that remain!
            var remained = category.CategoryAttributes.Where(ca => categoryW.CategoryAttributes.Any(caw => caw.Id == ca.Id)).ToList();
            foreach (var item in remained)
            {
                var catwAttr = categoryW.CategoryAttributes.Single(caw => caw.Id == item.Id);
                item.ActiveFilter = catwAttr.ActiveFilter;
            }

            //4. Add the new ones
            var newAttr = categoryW.CategoryAttributes.Where(caw => !category.CategoryAttributes.Any(ca => ca.Id == caw.Id)).ToList();
            foreach (var item in newAttr)
            {
                var catAttr = Mapper.Map<CategoryAttribute>(item);
                category.CategoryAttributes.Add(catAttr);
            }

            if (deleted == true)
            {
                //implement a way to delete the existing file
                //cdnService.DeleteFiles()

                category.ImagePath = null;
            }

            ITicket ticket = null;
            if (photo != null)
            {
                category.ImagePath = ImportantVariables.CategoriesPrefix + photo.FileName;

                List<HttpFile> files = new List<HttpFile>();
                HttpFile file = new HttpFile();
                file.Filename = category.ImagePath;
                file.Content = new byte[photo.ContentLength];
                await photo.InputStream.ReadAsync(file.Content, 0, photo.ContentLength);

                files.Add(file);

                if (files.Count > 0)
                {
                    bool success = await cdnService.SaveFiles(files);

                    if (success == false)
                    {
                        ticket = new Ticket(false, "Записването на изображения се провали!");
                    }
                }
            }

            rep.Save();

            return new Ticket(true);
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