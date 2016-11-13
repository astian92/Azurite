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
        private IShoppingCartWorker cartWorker;

        public ProductsController(IProductWorker worker, IShoppingCartWorker cartWorker)
        {
            this.worker = worker;
            this.cartWorker = cartWorker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(Guid id)
        {
            var product = worker.GetProduct(id);
            return View(product);
        }

        public ActionResult GetCategoryProducts(Guid categoryId)
        {
            var products = worker.GetProducts(categoryId);
            return PartialView(products);
        }

        public ActionResult GetPromoProducts()
        {
            var products = worker.GetPromoProducts();
            return PartialView(products);
        }
    }
}