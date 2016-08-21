using Azurite.Storehouse.Models.ViewModels;
using Azurite.Storehouse.Workers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using System.Web.Security;

namespace Azurite.Storehouse.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountWorker worker;

        public AccountController(IAccountWorker worker)
        {
            this.worker = worker;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = worker.Authenticate(model);

                if (isUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return Redirect(Url.Action<HomeController>(c => c.Index()));
                }
                else
                {
                    ModelState.AddModelError("Error", "Неправилно потребителско име или парола!");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Action<AccountController>(c => c.Login()));
        }
    }
}