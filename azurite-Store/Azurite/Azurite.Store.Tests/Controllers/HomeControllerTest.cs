using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Azurite.Store.Controllers;
using Azurite.Store.Workers.Contracts;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Azurite.Store.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<ICurrencyWorker> workerMock;

        private ICurrencyWorker worker
        {
            get
            {
                return this.workerMock.Object;
            }
        }

        private HomeController controller;

        public HomeControllerTest()
        {
            this.workerMock = new Mock<ICurrencyWorker>();
            this.controller = new HomeController(worker, new Mock<ILog>().Object);
            this.controller.ControllerContext = new ControllerContext(new Mock<HttpContextBase>().Object,
                new Mock<RouteData>().Object, new Mock<ControllerBase>().Object);
        }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
