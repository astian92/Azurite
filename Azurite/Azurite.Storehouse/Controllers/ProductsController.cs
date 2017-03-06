using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;

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
                entities = entities.Where(e => e.Model.ToLower().Contains(dtParams.SearchValue) ||
                                               e.CategoryName.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Name.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Price.ToString().Contains(dtParams.SearchValue));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else
            {
                //defaultOrder
                entities = entities.OrderBy(e => e.Model);
            }

            var data = entities
                .Skip(dtParams.Skip)
                .Take(dtParams.PageSize != 0 ? dtParams.PageSize : 10);

            var jsonResult = new JqueryListResult<ProductIndexViewModel>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords);

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
        public async Task<ActionResult> Add(ProductW productW, IEnumerable<HttpPostedFileBase> photos)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

            var ticket = await worker.Add(productW, photos);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Add Error!", ticket.Message);
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

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
        public async Task<ActionResult> Edit(ProductW productW, IEnumerable<HttpPostedFileBase> photos, IEnumerable<Guid> imageIds)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

            var ticket = await worker.Edit(productW, photos, imageIds);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Edit Error!", ticket.Message);
                ViewBag.CategoryId = new SelectList(worker.GetCategoriesDropDownItems(), "Value", "Text");
                return View(productW);
            }

            return Redirect(Url.Action<ProductsController>(c => c.Index()));
        }

        public async Task<ActionResult> Delete(Guid Id)
        {
            var ticket = await worker.Delete(Id);
            return Json(ticket);
        }

        private IQueryable<ProductIndexViewModel> Filter(IQueryable<ProductIndexViewModel> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Model);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Model);
                    }

                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.CategoryName);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.CategoryName);
                    }

                    break;
                case 2:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Name);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Name);
                    }

                    break;
                case 3:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Price);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Price);
                    }

                    break;
                default:
                    break;
            }

            return entities;
        }
    }
}