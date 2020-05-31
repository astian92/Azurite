using System;
using System.Web.Mvc;
using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using log4net;

namespace Azurite.Store.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private IShoppingCartWorker worker;

        public ShoppingCartController(IShoppingCartWorker worker, ILog logger)
            : base(logger)
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

        public ActionResult CartSummary()
        {
            var order = worker.GetCartSummary();
            return PartialView(order);
        }

        public ActionResult CheckOut()
        {
            var order = worker.GetOrder();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderW order)
        {
            if(ModelState.IsValid)
            {
                if(worker.SaveOrder(order))
                    return RedirectToAction("OrderSuccess");
                else
                    return RedirectToAction("OrderFail");
            }

            return CheckOut(order);
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

        public ActionResult OrderFail()
        {
            return View();
        }

        public JsonResult AddProduct(Guid id, int quantity)
        {
            worker.AddProduct(id, quantity);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveProduct(Guid id)
        {
            worker.RemoveProduct(id);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModifyProductQty(Guid id, int quantity)
        {
            worker.ModifyProductQty(id, quantity);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}