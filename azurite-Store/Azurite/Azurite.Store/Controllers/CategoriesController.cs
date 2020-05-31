using System;
using System.Web.Mvc;
using Azurite.Store.Workers.Contracts;
using log4net;

namespace Azurite.Store.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class CategoriesController : BaseController
    {
        ICategoryWorker worker;

        public CategoriesController(ICategoryWorker worker, ILog logger)
            : base(logger)
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