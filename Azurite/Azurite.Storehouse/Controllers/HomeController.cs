using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models.Infrastructure;
using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeWorker worker;

        public HomeController(IHomeWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}