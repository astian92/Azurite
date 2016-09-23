using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Models;
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
    public class CategoriesController : Controller
    {
        private ICategoryWorker worker;

        public CategoriesController(ICategoryWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategories()
        {
            var categories = worker.GetAll();
            var result = new JqueryListResult<CategoryW>(data: categories);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryW categoryW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                return View(categoryW);
            }

            if (categoryW.CategoryAttributes == null || categoryW.CategoryAttributes.Count() == 0)
            {
                ModelState.AddModelError("Missing attributes!", "Категорията задължително трябва да има атрибути!");
                return View(categoryW);
            }

            worker.Add(categoryW);
            return Redirect(Url.Action<CategoriesController>(c => c.Index()));
        }
    }
}