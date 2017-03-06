using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyWorker _worker;

        public CurrencyController(ICurrencyWorker worker)
        {
            this._worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCurrencies()
        {
            var currencies = _worker.GetAll();
            var result = new JqueryListResult<CurrencyCoursW>(data: currencies);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var currencyW = _worker.Get(Id);
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

            var ticket = _worker.Edit(currencyW);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Edit Error!", ticket.Message);
                return View(currencyW);
            }

            return Redirect(Url.Action<CurrencyController>(c => c.Index()));
        }
    }
}