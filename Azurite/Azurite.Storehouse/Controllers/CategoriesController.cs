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
    }
}