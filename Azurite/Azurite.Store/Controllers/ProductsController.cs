using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Store.Controllers
{
    public class ProductsController : Controller
    {
        private IProductWorker worker;

        public ProductsController(IProductWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}