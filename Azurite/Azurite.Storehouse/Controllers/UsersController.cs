using System;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersWorker worker;

        public UsersController(IUsersWorker worker)
        {
            this.worker = worker;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var users = worker.GetAll();
            var result = new JqueryListResult<UserW>(data: users);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserW userW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Има проблем с данните!");
                return View(userW);
            }

            var ticket = worker.Add(userW);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Add Error", ticket.Message);
                return View(userW);
            }

            return Redirect(Url.Action<UsersController>(c => c.Index()));
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            var userW = worker.Get(Id);
            return View(userW);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserW userW)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Има проблем с данните!");
                return View(userW);
            }

            var ticket = worker.Edit(userW);

            if (ticket.IsOK == false)
            {
                ModelState.AddModelError("Edit Error", ticket.Message);
                return View(userW);
            }

            return Redirect(Url.Action<UsersController>(c => c.Index()));
        }

        public ActionResult Delete(Guid Id)
        {
            var ticket = worker.Delete(Id);
            return Json(ticket);
        }
    }
}