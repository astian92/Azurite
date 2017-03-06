using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using AutoMapper;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Data;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Wrappers;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Services.Contracts;
using Azurite.Storehouse.Config.Constants;
using Azurite.Storehouse.Models.Http;

namespace Azurite.Storehouse.Workers.Implementations
{
    public class CategoriesWorker : ICategoriesWorker
    {
        private readonly IRepository<Category> _rep;
        private readonly IRepository<CategoryAttribute> _attrRep;
        private readonly ICdnService _cdnService;

        public CategoriesWorker(IRepository<Category> rep, IRepository<CategoryAttribute> attrRep, ICdnService cdnService)
        {
            this._rep = rep;
            this._attrRep = attrRep;
            this._cdnService = cdnService;
        }

        public CategoryW Get(Guid Id)
        {
            var cat = _rep.Get(Id);
            var catW = Mapper.Map<CategoryW>(cat);

            return catW;
        }

        public IQueryable<CategoryW> GetAll()
        {
            var categories = _rep.GetAll();
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
            var categories = _rep.GetAll();
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
                    bool success = await _cdnService.SaveFiles(files);

                    if (success == false)
                    {
                        ticket = new Ticket(false, "Записването на изображения се провали!");
                    }
                }
            }

            _rep.Add(category);
            _rep.Save();

            if (ticket == null)
            {
                ticket = new Ticket(true);
            }

            return ticket;
        }

        public async Task<ITicket> Edit(CategoryW categoryW, HttpPostedFileBase photo, bool deleted)
        {
            try
            {
                var category = _rep.Get(categoryW.Id);

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
                _attrRep.RemoveRange(removed);

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
                        bool success = await _cdnService.SaveFiles(files);

                        if (success == false)
                        {
                            ticket = new Ticket(false, "Записването на изображения се провали!");
                        }
                    }
                }

                _rep.Save();

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

        public ITicket Delete(Guid Id)
        {
            try
            {
                _rep.Remove(Id);
                _rep.Save();
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