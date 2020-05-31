using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Azurite.Store.Workers.Contracts;
using log4net;

namespace Azurite.Store.Controllers
{
    public class HomeController : BaseController
    {
        private ICurrencyWorker worker;

        public HomeController(ICurrencyWorker worker, ILog logger)
            : base(logger)
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
                builder.Append(currencyW.Id + "|" + currencyW.Code + "|" + currencyW.Value.ToString("0.######", CultureInfo.InvariantCulture) + "|" + currencyW.Sign);

                var cookie = new HttpCookie("Currency");
                cookie.Value = HttpUtility.UrlEncode(builder.ToString());
                Response.Cookies.Add(cookie);
            }

            return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "Index");
        }
    }
}