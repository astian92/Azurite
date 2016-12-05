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

        public ActionResult CartSummary()
        {
            var order = worker.GetCartSummary();
            return PartialView(order);
        }

        public ActionResult Billing()
        {
            var customerW = new CustomerW();
            customerW.Id = Guid.NewGuid();

            return View(customerW);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Billing(CustomerW customerW)
        {
            if(ModelState.IsValid)
            {
                if(worker.CheckOutOrder(customerW))
                    return RedirectToAction("CheckOut");
                else
                    return RedirectToAction("Index");
            }

            return View(customerW);
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
    }
}