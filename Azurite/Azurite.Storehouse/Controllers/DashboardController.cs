using AutoMapper.QueryableExtensions;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Models;
using Azurite.Storehouse.Models.Http;
using Azurite.Storehouse.Models.Infrastructure;
using Azurite.Storehouse.Models.ViewModels;
using Azurite.Storehouse.Services.Contracts;
using Azurite.Storehouse.Services.Implementations;
using Azurite.Storehouse.Wrappers.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MarketPlaceEntities db;

        public DashboardController(IStorehouseDbFactory factory)
        {
            this.db = factory.CreateConcrete();
        }

        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            //counts
            model.OrdersCounts.All = db.Orders.Count();
            model.OrdersCounts.Issued = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Issued).Count();
            model.OrdersCounts.InProcessing = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.InProcessing).Count();
            model.OrdersCounts.Completed = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Completed).Count();
            model.OrdersCounts.Archived = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Archived).Count();
            model.OrdersCounts.Cancelled = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Cancelled).Count();

            //orders
            model.InProgressOrders = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.InProcessing)
                .ProjectTo<MiniOrder>().ToList();
            model.IssuedOrders = db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Issued)
                .ProjectTo<MiniOrder>().ToList();

            //products
            model.DecreasingQuantityProducts = db.Products.Where(p => p.Quantity <= 5)
                .ProjectTo<MiniProduct>().ToList();

            return View(model);
        }

    }
}