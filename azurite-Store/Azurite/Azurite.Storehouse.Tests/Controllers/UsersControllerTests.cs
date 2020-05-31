using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Azurite.Storehouse.Controllers;
using Azurite.Storehouse.Models.Helpers.Datatables;
using Azurite.Storehouse.Workers.Contracts;
using Azurite.Storehouse.Wrappers;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Azurite.Storehouse.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        private Mock<IUsersWorker> workerMock;

        private IUsersWorker worker
        {
            get
            {
                return this.workerMock.Object;
            }
        }

        private UsersController controller;

        public UsersControllerTests()
        {
            this.workerMock = new Mock<IUsersWorker>();
            this.workerMock.Setup(m => m.GetAll()).Returns(new List<UserW>()
            {
                new UserW() { Id = Guid.NewGuid(), FirstName = "FN", LastName = "LN", Password = "PS", Username = "UN" },
                new UserW() { Id = Guid.NewGuid(), FirstName = "FN2", LastName = "LN2", Password = "PS2", Username = "UN2" }
            }.AsQueryable());
            this.workerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new UserW() { Username = "Usn" });

            this.controller = new UsersController(this.worker, new Mock<ILog>().Object);
            var urlMock = new Mock<UrlHelper>();
            urlMock.Setup(m => m.Action(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>()))
                .Returns<string, string, RouteValueDictionary>((a, c, r) => c + "/" + a);

            this.controller.Url = urlMock.Object;
        }

        [TestMethod]
        public void IndexTest()
        {
            var actual = this.controller.Index();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var actual = this.controller.GetUsers();
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(JsonResult));

            var result = actual as JsonResult;
            var jqueryList = result.Data as JqueryListResult<UserW>;

            Assert.IsNotNull(jqueryList);
            Assert.AreEqual(2, jqueryList.data.Count());
        }

        [TestMethod]
        public void AddGetTest()
        {
            var actual = this.controller.Add();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void AddPostTestNoModel()
        {
            //force the model state to have an error
            this.controller.ModelState.AddModelError("ErrorOld", "EmptyModel");
            var actual = this.controller.Add(null);

            //assert model state has an additional error
            int errors = 0;
            foreach (var state in this.controller.ModelState.Values)
            {
                errors += state.Errors.Count;
            }

            Assert.AreEqual(2, errors);

            //assert we got back a view
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void AddPostTestWithValidModel()
        {
            var model = new UserW() { Username = "un" };
            var actual = this.controller.Add(model);

            workerMock.Verify(m => m.Add(model));

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectResult));
        }

        [TestMethod]
        public void EditGetTest()
        {
            var actual = this.controller.Edit(Guid.NewGuid());

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));

            var view = actual as ViewResult;
            Assert.IsNotNull(view.Model);
            Assert.IsInstanceOfType(view.Model, typeof(UserW));
        }

        [TestMethod]
        public void EditPostTestNoModel()
        {
            //force the model state to have an error
            this.controller.ModelState.AddModelError("ErrorOld", "EmptyModel");
            var actual = this.controller.Edit(null);

            //assert model state has an additional error
            int errors = 0;
            foreach (var state in this.controller.ModelState.Values)
            {
                errors += state.Errors.Count;
            }

            Assert.AreEqual(2, errors);

            //assert we got back a view
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }

        [TestMethod]
        public void EditPostTestWithValidModel()
        {
            var model = new UserW() { Username = "un" };
            var actual = this.controller.Edit(model);

            workerMock.Verify(m => m.Edit(model));

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(RedirectResult));
        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = Guid.NewGuid();
            var actual = this.controller.Delete(id);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(JsonResult));

            workerMock.Verify(m => m.Delete(id));
        }
    }
}
