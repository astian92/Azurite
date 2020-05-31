using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using System.Web.Security;
using Azurite.Storehouse.Models.ViewModels;
using Azurite.Storehouse.Workers.Contracts;
using log4net;

namespace Azurite.Storehouse.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountWorker worker;

        public AccountController(IAccountWorker worker, ILog logger)
            : base (logger)
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
                    return Redirect(Url.Action<DashboardController>(c => c.Index()));
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