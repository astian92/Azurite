using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Azurite.Store.Controllers;
using Azurite.Store.Workers.Contracts;

namespace Azurite.Store.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _controller;

        private Mock<ICurrencyWorker> _workerMock;

        public HomeControllerTest()
        {
            _workerMock = new Mock<ICurrencyWorker>();
            _controller = new HomeController(Worker);
            _controller.ControllerContext = new ControllerContext(new Mock<HttpContextBase>().Object,
                new Mock<RouteData>().Object, new Mock<ControllerBase>().Object);
        }

        private ICurrencyWorker Worker
        {
            get
            {
                return _workerMock.Object;
            }
        }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Act
            ViewResult result = _controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
