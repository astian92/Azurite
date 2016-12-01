using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using PagedList;
using System;
using System.Linq;
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

        public ActionResult Details(Guid id)
        {
            var product = worker.GetProduct(id);
            return View(product);
        }

        public ActionResult GetCategoryProducts(Guid categoryId, string search = "", int page = 1, int pageSize = 12)
        {
            page--;
            var products = worker.GetProducts(categoryId);

            var filteredByModel = products.Where(x => x.Model.IndexOf(search) != -1);
            var pagedProducts = new StaticPagedList<ProductW>(filteredByModel.OrderBy(x => x.Model).Skip(page * pageSize).Take(pageSize), page + 1, pageSize, filteredByModel.Count());

            ViewBag.categoryId = categoryId;

            return PartialView(pagedProducts);
        }

        public ActionResult GetPromoProducts()
        {
            var products = worker.GetPromoProducts();
            return PartialView(products);
        }

        public ActionResult GetAllPromoProducts()
        {
            var products = worker.GetAllPromoProducts();
            return PartialView(products);
        }

        public ActionResult GetRelatedProducts(Guid categoryId)
        {
            var products = worker.GetRelatedProducts(categoryId);
            return PartialView(products);
        }
    }
}