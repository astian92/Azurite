using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Language(string LanguageAbbr = "bg")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbr);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbr);

            var cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbr;
            Response.Cookies.Add(cookie);

            return View("Index");
        }
    }
}