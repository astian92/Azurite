using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace Azurite.Storehouse.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsWorker worker;

        public ProductsController(IProductsWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        //Number, Name, Price and Quantity
        public ActionResult GetProducts()
        {
            var dtParams = new DtParameters(Request);

            var entities = worker.GetAll();
            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.Number.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Name.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Price.ToString().Contains(dtParams.SearchValue));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else //defaultOrder
            {
                entities = entities.OrderBy(e => e.Number);
            }

            var data = entities
                .Skip(dtParams.Skip)
                .Take(dtParams.PageSize != 0 ? dtParams.PageSize : 10);

            var jsonResult = new JqueryListResult<ProductIndexViewModel>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords
                );

            return Json(jsonResult);
        }

        [HttpGet]
        public ActionResult GetCategoryAttributes(Guid categoryId)
        {
            var attributes = worker.GetCategoryAttributes(categoryId);
            return Json(attributes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductW productW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

            worker.Add(productW);
            return Redirect(Url.Action<ProductsController>(c => c.Index()));
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
            var productW = worker.Get(Id);

            return View(productW);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductW productW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

            worker.Edit(productW);
            return Redirect(Url.Action<ProductsController>(c => c.Index()));
        }

        public ActionResult Delete(Guid Id)
        {
            var ticket = worker.Delete(Id);
            return Json(ticket);
        }

        private IQueryable<ProductIndexViewModel> Filter(IQueryable<ProductIndexViewModel> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Number);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Number);
                    }
                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Name);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Name);
                    }
                    break;
                case 2:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Price);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Price);
                    }
                    break;
                case 3:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Quantity);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Quantity);
                    }
                    break;
                default:
                    break;
            }

            return entities;
        }

        
    }
}