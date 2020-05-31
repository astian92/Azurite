using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using log4net;

namespace Azurite.Storehouse.Controllers
{
    public class CategoriesController : BaseController
    {
        private ICategoriesWorker worker;

        public CategoriesController(ICategoriesWorker worker, ILog logger)
            : base(logger)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategories()
        {
            var categories = worker.GetAllWithoutParents();
            var result = new JqueryListResult<CategoryIndexViewModel>(data: categories);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CategoryW categoryW, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
                return View(categoryW);
            }

            //if (categoryW.CategoryAttributes == null || categoryW.CategoryAttributes.Count() == 0)
            //{
            //    ModelState.AddModelError("Missing attributes!", "Категорията задължително трябва да има атрибути!");
            //    ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
            //    return View(categoryW);
            //}

            var ticket = await worker.Add(categoryW, photo);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Add Error!", ticket.Message);
                ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
                return View(categoryW);
            }

            return Redirect(Url.Action<CategoriesController>(c => c.Index()));
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            var categoryW = worker.Get(Id);
            ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text", categoryW.ParentId);

            return View(categoryW);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryW categoryW, HttpPostedFileBase photo, bool deleted)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
                return View(categoryW);
            }

            //if (categoryW.CategoryAttributes == null || categoryW.CategoryAttributes.Count() == 0)
            //{
            //    ModelState.AddModelError("Missing attributes!", "Категорията задължително трябва да има атрибути!");
            //    ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
            //    return View(categoryW);
            //}

            var ticket = await worker.Edit(categoryW, photo, deleted);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Add Error!", ticket.Message);
                ViewBag.ParentId = new SelectList(GetCategoriesDropDownItems(), "Value", "Text");
                return View(categoryW);
            }

            return Redirect(Url.Action<CategoriesController>(c => c.Index()));
        }

        public ActionResult Delete(Guid Id)
        {
            var ticket = worker.Delete(Id);
            return Json(ticket);
        }

        private List<DropDownItem> GetCategoriesDropDownItems()
        {
            var items = worker.GetAll()
                .Select(c => new DropDownItem(c.Id.ToString(), c.Name))
                .ToList();

            items.Insert(0, new DropDownItem(Guid.Empty.ToString(), "Няма"));

            return items;
        }
    }
}