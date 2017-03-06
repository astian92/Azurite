using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Azurite.Storehouse.Controllers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        private CustomersController _controller;

        private Mock<ICustomersWorker> _workerMock;

        public CustomersControllerTests()
        {
            this._workerMock = new Mock<ICustomersWorker>();
            this._workerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new CustomerW() { FirstName = "customer" });
            this._workerMock.Setup(m => m.GetAll()).Returns(new List<CustomerIndexViewModel>()
            {
                new CustomerIndexViewModel() { FirstName = "Fn1" },
                new CustomerIndexViewModel() { FirstName = "Fn2" },
            }.AsQueryable());

            this._controller = new CustomersController(Worker);
            this._controller.ControllerContext = new ControllerContext(new Mock<HttpContextBase>().Object, new Mock<RouteData>().Object, new Mock<ControllerBase>().Object);
        }

        private ICustomersWorker Worker
        {
            get
            {
                return this._workerMock.Object;
            }
        }

        [TestMethod]
        public void IndexTest()
        {
            var actual = _controller.Index();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void GetCustomersTest()
        {
            var actual = _controller.GetCustomers();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(JsonResult));

            var jres = actual as JsonResult;
            var jqueryList = jres.Data as JqueryListResult<CustomerIndexViewModel>;

            Assert.IsNotNull(jqueryList);
            Assert.AreEqual(2, jqueryList.Data.Count());
            Assert.AreEqual(2, jqueryList.RecordsFiltered);
            Assert.AreEqual(2, jqueryList.RecordsTotal);
        }

        [TestMethod]
        public void DetailsTest()
        {
            var actual = _controller.Details(Guid.NewGuid());

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));

            var view = actual as ViewResult;
            var model = view.Model as CustomerW;

            Assert.IsNotNull(model);
        }
    }
}
