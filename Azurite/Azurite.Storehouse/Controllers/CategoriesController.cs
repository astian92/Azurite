using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Models;
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
            var categories = worker.GetAll();
            return View(categories);
        }

        public void ChangeToEnglish()
        {
            LanguageChecker.IsEnglish = true;
        }
    }
}