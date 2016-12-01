using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Store.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class CategoriesController : Controller
    {
        ICategoryWorker worker;

        public CategoriesController(ICategoryWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index(Guid id)
        {
            var category = worker.GetCategory(id);
            return View(category);
        }

        public ActionResult Promotions()
        {
            return View();
        }

        public ActionResult GetBaseCategories()
        {
            var categories = worker.GetBaseCategories();
            return PartialView(categories);
        }

        public ActionResult GetSubCategories(Guid categoryId)
        {
            var categories = worker.GetSubCategories(categoryId);
            return PartialView(categories);
        }

        public ActionResult GetCategoryTree(Guid? categoryId)
        {
            var categories = worker.GetAll();

            ViewBag.categoryId = categoryId ?? Guid.Empty;
            return PartialView(categories);
        }

        public ActionResult GetCategoryMenu()
        {
            var categories = worker.GetBaseCategories();
            return PartialView(categories);
        }

        public ActionResult GetCategoryAttr(Guid categoryId)
        {
            var categoryAttr = worker.GetCategoryAttr(categoryId);
            return PartialView(categoryAttr);
        }
    }
}