using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartWorker worker;

        public ShoppingCartController(IShoppingCartWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = worker.GetShoppingCart();
            return PartialView(products);
        }

        public JsonResult AddProduct(Guid id)
        {
            worker.AddProduct(id);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}