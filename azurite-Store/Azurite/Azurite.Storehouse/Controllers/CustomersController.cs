using System;
using System.Linq;
using System.Web.Mvc;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using log4net;

namespace Azurite.Storehouse.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ICustomersWorker worker;

        public CustomersController(ICustomersWorker worker, ILog logger)
            : base(logger)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomers()
        {
            var dtParams = new DtParameters(Request);

            var entities = worker.GetAll();
            int totalRecords = entities.Count();

            if (dtParams.IsBeingSearched)
            {
                entities = entities.Where(e => e.FirstName.ToLower().Contains(dtParams.SearchValue) ||
                                               e.LastName.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Phone.ToLower().Contains(dtParams.SearchValue) ||
                                               e.Email.ToLower().Contains(dtParams.SearchValue));
            }

            int filteredRecords = entities.Count();

            if (dtParams.IsBeingFiltered)
            {
                entities = Filter(entities, dtParams.FilterColIndex, dtParams.FilterAsc);
            }
            else //defaultOrder
            {
                entities = entities.OrderBy(e => e.FirstName);
            }

            var data = entities
                .Skip(dtParams.Skip)
                .Take(dtParams.PageSize != 0 ? dtParams.PageSize : 10);

            var jsonResult = new JqueryListResult<CustomerIndexViewModel>(
                    data,
                    dtParams.Draw,
                    filteredRecords,
                    totalRecords
                );

            return Json(jsonResult);
        }

        public ActionResult Details(Guid Id)
        {
            var customerW = worker.Get(Id);
            return View(customerW);
        }

        private IQueryable<CustomerIndexViewModel> Filter(IQueryable<CustomerIndexViewModel> entities, int colIndex, bool asc)
        {
            switch (colIndex)
            {
                case 0:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.FirstName);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.FirstName);
                    }
                    break;
                case 1:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.LastName);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.LastName);
                    }
                    break;
                case 2:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Phone);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Phone);
                    }
                    break;
                case 3:
                    if (asc == true)
                    {
                        entities = entities.OrderBy(e => e.Email);
                    }
                    else
                    {
                        entities = entities.OrderByDescending(e => e.Email);
                    }
                    break;
                default:
                    break;
            }

            return entities;
        }
    }
}