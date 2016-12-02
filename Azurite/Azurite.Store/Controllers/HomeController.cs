using Azurite.Store.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Store.Controllers
{
    public class HomeController : Controller
    {
        private ICurrencyWorker worker;

        public HomeController(ICurrencyWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Language(string LanguageAbbr = "bg")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbr);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbr);
            
            var cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbr;
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "Index");
        }

        public ActionResult Currency()
        {
            var currencies = worker.GetAllCurrencies();
            return PartialView(currencies);
        }

        public ActionResult ChangeCurrency(int currencyId)
        {
            var currencyW = worker.GetCurrency(currencyId);
            if (currencyW != null)
            {
                var builder = new StringBuilder();
                builder.Append(currencyW.Id + "|" + currencyW.Code + "|" + currencyW.Value + "|" + currencyW.Sign);

                var cookie = new HttpCookie("Currency");
                cookie.Value = builder.ToString();
                Response.Cookies.Add(cookie);
            }

            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "Index");
        }
    }
}