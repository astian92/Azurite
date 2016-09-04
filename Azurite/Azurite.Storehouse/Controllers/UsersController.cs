using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var users = worker.GetAll();
            return View(users);
        }

        public ActionResult GetUsers()
        {
            var users = worker.GetAll();
            var result = new JqueryListResult<UserW>(data: users);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}