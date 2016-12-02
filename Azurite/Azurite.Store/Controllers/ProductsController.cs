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

        public ActionResult GetCategoryProducts(Guid categoryId, string search = "", int orderBy = 1, int page = 1, int pageSize = 12)
        {
            page--;
            var products = worker.GetProducts(categoryId);

            var filteredByModel = products.Where(x => x.Model.ToLower().IndexOf(search.ToLower()) != -1);

            var orderedProducts = Enumerable.Empty<ProductW>();
            switch (orderBy)
            {
                case 1:
                    orderedProducts = filteredByModel.OrderBy(x => x.Price);
                    break;
                case 2:
                    orderedProducts = filteredByModel.OrderByDescending(x => x.Price);
                    break;
                case 3:
                    orderedProducts = filteredByModel.OrderByDescending(x => x.Discount);
                    break;
                default:
                    orderedProducts = filteredByModel.OrderBy(x => x.Price);
                    break;
            }

            var pagedProducts = new StaticPagedList<ProductW>(orderedProducts.Skip(page * pageSize).Take(pageSize), page + 1, pageSize, filteredByModel.Count());

            ViewBag.categoryId = categoryId;
            ViewBag.search = search;
            ViewBag.orderBy = orderBy;

            return PartialView(pagedProducts);
        }

        public ActionResult GetPromoProducts()
        {
            var products = worker.GetPromoProducts();
            return PartialView(products);
        }

        public ActionResult GetAllPromoProducts(string search = "", int orderBy = 1, int page = 1, int pageSize = 12)
        {
            page--;
            var products = worker.GetAllPromoProducts();

            var filteredByModel = products.Where(x => x.Model.ToLower().IndexOf(search.ToLower()) != -1);

            var orderedProducts = Enumerable.Empty<ProductW>();
            switch (orderBy)
            {
                case 1:
                    orderedProducts = filteredByModel.OrderBy(x => x.Price);
                    break;
                case 2:
                    orderedProducts = filteredByModel.OrderByDescending(x => x.Price);
                    break;
                case 3:
                    orderedProducts = filteredByModel.OrderByDescending(x => x.Discount);
                    break;
                default:
                    orderedProducts = filteredByModel.OrderBy(x => x.Price);
                    break;
            }

            var pagedProducts = new StaticPagedList<ProductW>(orderedProducts.Skip(page * pageSize).Take(pageSize), page + 1, pageSize, filteredByModel.Count());

            ViewBag.search = search;
            ViewBag.orderBy = orderBy;

            return PartialView(pagedProducts);
        }

        public ActionResult GetRelatedProducts(Guid categoryId)
        {
            var products = worker.GetRelatedProducts(categoryId);
            return PartialView(products);
        }
    }
}