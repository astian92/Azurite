using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Azurite.Storehouse.Models;
using Azurite.Storehouse.Models.Helpers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using log4net;

namespace Azurite.Storehouse.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrdersWorker worker;

        public OrdersController(IOrdersWorker worker, ILog logger)
            : base(logger)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            ViewBag.OrderStatuses = new SelectList(GetOrderStatusesDropDownItemsWithAll(), "Value", "Text");
            return View();
        }
        
        public ActionResult GetOrders(int orderStatusId)
        {
            var dtParams = new DtParameters(Request);

            var entities = worker.GetAllVm();

            if (orderStatusId > 0) 
            {
                entities = entities.Where(o => o.StatusId == orderStatusId);
            }
            else //All = All without the cancelled and archived ones
            {
                entities = entities.Where(o => o.StatusId != (int)OrderStatuses.Cancelled && o.StatusId != (int)OrderStatuses.Archived);
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
            ViewBag.StatusId = new SelectList(GetOrderStatusesDropDownItems(), "Value", "Text");
            var orderW = worker.Get(Id);

            return View(orderW);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Guid orderId, int statusId, string notes)
        {
            var ticket = worker.Update(orderId, statusId, notes);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("QuantityError", ticket.Message);
                //return Redirect(Url.Action<OrdersController>(c => c.Details(orderId)));

                ViewBag.StatusId = new SelectList(GetOrderStatusesDropDownItems(), "Value", "Text");
                var orderW = worker.Get(orderId);

                return View("Details", orderW);
            }

            return Redirect(Url.Action<OrdersController>(c => c.Index()));
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

        private List<DropDownItem> GetOrderStatusesDropDownItemsWithAll()
        {
            var items = this.GetOrderStatusesDropDownItems();
            items.Insert(0, new DropDownItem("0", "Всички"));

            return items;
        }

        private List<DropDownItem> GetOrderStatusesDropDownItems()
        {
            var items = worker.GetOrderStatuses()
                .Select(c => new DropDownItem(c.Id.ToString(), c.DisplayName))
                .ToList();

            return items;
        }



    }
}