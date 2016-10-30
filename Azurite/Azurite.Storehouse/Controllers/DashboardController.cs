using Azurite.Storehouse.Models.Http;
using Azurite.Storehouse.Services.Contracts;
using Azurite.Storehouse.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Azurite.Storehouse.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICdnService cdnService;

        public DashboardController(ICdnService service)
        {
            this.cdnService = service;
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public async Task<ActionResult> TestSend(string text)
        {
            var httpFile = new HttpFile();
            httpFile.Content = new byte[100];
            httpFile.Filename = "New fkn file";
            httpFile.ContentType = "image/png";

            var httpService = new HttpService();
            var result = await httpService.PostAsync<int>(new Uri("http://localhost:8696/cdn/save"), httpFile);

            return Json("so?", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TestDeleteFile()
        {
            var result = await cdnService.DeleteFile(Guid.Parse("30BE0B2B-E502-4575-AA44-FA679E342A5E"));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TestDeleteFiles()
        {
            var result = await cdnService.DeleteFiles(new Guid[] { Guid.NewGuid(), Guid.NewGuid() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}