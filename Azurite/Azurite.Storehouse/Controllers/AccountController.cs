using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace Azurite.Storehouse.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectToIndex()
        {
            //return Redirect("Index");
            return Redirect(Url.Action<AccountController>(c => c.Index()));
        }
    }
}