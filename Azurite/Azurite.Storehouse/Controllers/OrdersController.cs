using Azurite.Storehouse.Models;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersWorker worker;

        public OrdersController(IOrdersWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            ViewBag.OrderStatuses = new SelectList(GetOrderStatusesDropDownItems(), "Value", "Text");
            return View();
        }
        
        public ActionResult GetOrders(int orderStatusId)
        {
            var dtParams = new DtParameters(Request);

            var entities = worker.GetAllVm();

            if (orderStatusId > 0) //filter only if a status is selected, if it is 0 => all
            {
                entities = entities.Where(o => o.StatusId == orderStatusId);
            }

            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.CustomerName.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Total.ToString().Contains(dtParams.SearchValue));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else //defaultOrder
            {
                entities = entities.OrderByDescending(e => e.Date);
            }

            var data = entities
                .Skip(dtParams.Skip)
                .Take(dtParams.PageSize != 0 ? dtParams.PageSize : 10);

            var jsonResult = new JqueryListResult<OrderViewModel>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords
                );

            return Json(jsonResult);
        }

        public ActionResult Details(Guid Id)
        {
            return View();
        }

        private IQueryable<OrderViewModel> Filter(IQueryable<OrderViewModel> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.StatusId);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.StatusId);
                    }
                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.CustomerName);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.CustomerName);
                    }
                    break;
                case 2:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Total);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Total);
                    }
                    break;
                case 3:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Date);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Date);
                    }
                    break;
                default:
                    break;
            }

            return entities;
        }

        private List<DropDownItem> GetOrderStatusesDropDownItems()
        {
            var items = worker.GetOrderStatuses()
                .Select(c => new DropDownItem(c.Id.ToString(), c.DisplayName))
                .ToList();

            items.Insert(0, new DropDownItem("0", "Всички"));

            return items;
        }
    }
}