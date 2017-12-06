using Azurite.Storehouse.Controllers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Azurite.Storehouse.Tests.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        private Mock<ICustomersWorker> workerMock;

        private ICustomersWorker worker
        {
            get
            {
                return this.workerMock.Object;
            }
        }

        private CustomersController controller;

        public CustomersControllerTests()
        {
            this.workerMock = new Mock<ICustomersWorker>();
            this.workerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new CustomerW() { FirstName = "customer" });
            this.workerMock.Setup(m => m.GetAll()).Returns(new List<CustomerIndexViewModel>()
            {
                new CustomerIndexViewModel() { FirstName = "Fn1" },
                new CustomerIndexViewModel() { FirstName = "Fn2" },
            }.AsQueryable());

            this.controller = new CustomersController(worker);
            this.controller.ControllerContext = new ControllerContext(new Mock<HttpContextBase>().Object, 
                new Mock<RouteData>().Object, new Mock<ControllerBase>().Object);
        }

        [TestMethod]
        public void IndexTest()
        {
            var actual = controller.Index();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void GetCustomersTest()
        {
            var actual = controller.GetCustomers();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(JsonResult));

            var jres = actual as JsonResult;
            var jqueryList = jres.Data as JqueryListResult<CustomerIndexViewModel>;

            Assert.IsNotNull(jqueryList);
            Assert.AreEqual(2, jqueryList.data.Count());
            Assert.AreEqual(2, jqueryList.recordsFiltered);
            Assert.AreEqual(2, jqueryList.recordsTotal);
        }

        [TestMethod]
        public void DetailsTest()
        {
            var actual = controller.Details(Guid.NewGuid());

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));

            var view = actual as ViewResult;
            var model = view.Model as CustomerW;

            Assert.IsNotNull(model);
        }
    }
}
