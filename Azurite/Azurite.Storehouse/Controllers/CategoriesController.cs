using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Controllers
{
    public class CategoriesController : Controller
    {
        private ITranslatedRepository<CategoryW, Guid> rep;

        public CategoriesController(ITranslatedRepository<CategoryW, Guid> rep)
        {
            this.rep = rep;
        }

        public ActionResult Index()
        {
            var model = rep.GetAll();
            return View(model);
        }
    }
}