using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace Azurite.Storehouse.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyWorker worker;

        public CurrencyController(ICurrencyWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCurrencies()
        {
            var currencies = worker.GetAll();
            var result = new JqueryListResult<CurrencyCoursW>(data: currencies);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var currencyW = worker.Get(Id);
            return View(currencyW);
        }

        [HttpPost]
        public ActionResult Edit(CurrencyCoursW currencyW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid Sate!", "Липсват данни!");
                return View(currencyW);
            }

            var ticket = worker.Edit(currencyW);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Edit Error!", ticket.Message);
                return View(currencyW);
            }

            return Redirect(Url.Action<CurrencyController>(c => c.Index()));
        }
    }
}