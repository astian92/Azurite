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
using System.Data.Entity;
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

        public ActionResult YearIncome()
        {
            var ordersThisYear = Orders().Where(o => o.Date.Year == DateTime.Today.Year)
                .ToList();

            var nreport = new NumbersReport();
            nreport.Number = ((decimal?)ordersThisYear.Sum(o => o.Total)) ?? 0m;

            for (int i = 1; i <= 12; i++)
            {
                decimal month = 0;
                var monthly = ordersThisYear.Where(o => o.Date.Month == i);

                if (monthly.Any())
                {
                    month = (decimal)monthly.Sum(o => o.Total);
                }
                    
                nreport.Report.Add(month);
            }

            return Json(nreport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthIncome()
        {
            var ordersThisMonth = Orders().Where(o => o.Date.Year == DateTime.Today.Year &&  o.Date.Month == DateTime.Today.Month)
                .ToList();

            var nreport = new NumbersReport();
            nreport.Number = ((decimal?)ordersThisMonth.Sum(o => o.Total)) ?? 0m;

            for (int i = 0; i < 30; i++)
            {
                decimal day = 0;
                var ordersThatDay = ordersThisMonth.Where(o => o.Date.Date == DateTime.Today.AddDays(-i));

                if (ordersThatDay.Any())
                {
                    day = (decimal)ordersThatDay.Sum(o => o.Total);
                }

                nreport.Report.Add(day);
            }
            nreport.Report.Reverse(); //so today is the last day in the graph

            return Json(nreport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WeeklyIncome()
        {
            DateTime weekStart = DateTime.Today; //monday
            DateTime weekEnd = DateTime.Today; //sunday

            while (weekStart.DayOfWeek != DayOfWeek.Monday)
            {
                weekStart = weekStart.AddDays(-1);
            }

            while (weekEnd.DayOfWeek != DayOfWeek.Sunday)
            {
                weekEnd = weekEnd.AddDays(1);
            }

            var ordersThisWeek = Orders()
                .Where(o => DbFunctions.TruncateTime(o.Date) >= weekStart && DbFunctions.TruncateTime(o.Date) <= weekEnd)
                .ToList();
            var nreport = new NumbersReport();
            nreport.Number = ((decimal?)ordersThisWeek.Sum(o => o.Total)) ?? 0m;

            for (int i = 1; i <= 7; i++)
            {
                DayOfWeek weekDay = DayOfWeek.Monday;

                switch (i)
                {
                    case 1:
                        weekDay = DayOfWeek.Monday;
                        break;
                    case 2:
                        weekDay = DayOfWeek.Tuesday;
                        break;
                    case 3:
                        weekDay = DayOfWeek.Wednesday;
                        break;
                    case 4:
                        weekDay = DayOfWeek.Thursday;
                        break;
                    case 5:
                        weekDay = DayOfWeek.Friday;
                        break;
                    case 6:
                        weekDay = DayOfWeek.Saturday;
                        break;
                    case 7:
                        weekDay = DayOfWeek.Sunday;
                        break;
                }

                decimal day = 0;
                var dayOrder = ordersThisWeek.Where(o => o.Date.DayOfWeek == weekDay);

                if (dayOrder.Any())
                {
                    day = (decimal)dayOrder.Sum(o => o.Total);
                }

                nreport.Report.Add(day);
            }

            return Json(nreport, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<Order> Orders()
        {
            return db.Orders.Where(o => o.StatusId == (int)OrderStatuses.Completed ||
                o.StatusId == (int)OrderStatuses.Archived);
        }
    }
}