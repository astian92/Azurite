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
using System.Web.Mvc;
using System.Web.Routing;

namespace Azurite.Storehouse.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private Mock<ICategoriesWorker> workerMock;

        private ICategoriesWorker worker
        {
            get
            {
                return this.workerMock.Object;
            }
        }

        private CategoriesController controller;

        public CategoriesControllerTests()
        {
            this.workerMock = new Mock<ICategoriesWorker>();
            var catList = new List<CategoryW>()
            {
                new CategoryW() { Name = "Cat1", NameEN = "Cat1", Description = "Desc1", DescriptionEN = "Descr1" },
                new CategoryW() { Name = "Cat2", NameEN = "Cat2", Description = "Desc2", DescriptionEN = "Descr2" }
            };
            var catViewModelList = new List<CategoryIndexViewModel>()
            {
                new CategoryIndexViewModel() { Name = "Cat1", NameEN = "Cat1", Description = "Desc1", DescriptionEN = "Descr1" },
                new CategoryIndexViewModel() { Name = "Cat2", NameEN = "Cat2", Description = "Desc2", DescriptionEN = "Descr2" }
            };

            this.workerMock.Setup(m => m.GetAll()).Returns(catList.AsQueryable());
            this.workerMock.Setup(m => m.GetAllWithoutParents()).Returns(catViewModelList.AsQueryable());

            this.workerMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new CategoryW() { Name = "Name" });

            this.controller = new CategoriesController(worker);
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
        public void GetCategoriesTest()
        {
            var actual = this.controller.GetCategories();
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(JsonResult));

            var result = actual as JsonResult;
            var jqueryList = result.Data as JqueryListResult<CategoryIndexViewModel>;

            Assert.IsNotNull(jqueryList);
            Assert.AreEqual(2, jqueryList.data.Count());
        }

        [TestMethod]
        public void AddGetTest()
        {
            var actual = this.controller.Add();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));

            var view = actual as ViewResult;
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task AddPostTestNoModel()
        {
            //force the model state to have an error
            this.controller.ModelState.AddModelError("ErrorOld", "EmptyModel");
            var actual = await this.controller.Add(null, null);

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

            var view = actual as ViewResult;
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task AddPostTestWithNoCategoryAttributes()
        {
            var model = new CategoryW() { Name = "NameN" };
            var actual = this.controller.Add(model, null);

            int errors = 0;
            foreach (var state in this.controller.ModelState.Values)
            {
                errors += state.Errors.Count;
            }

            Assert.AreEqual(1, errors);

            //assert we got back a view
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));

            var view = await actual as ViewResult;
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task AddPostTestWithValidModel()
        {
            var model = new CategoryW() { Name = "NameN", CategoryAttributes = new List<CategoryAttributeW>()
            {
                new CategoryAttributeW() { AttributeName = "AN" }
            }};
            var actual = await this.controller.Add(model, null);

            workerMock.Verify(m => m.Add(model, null));

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
            Assert.IsInstanceOfType(view.Model, typeof(CategoryW));
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task EditPostTestNoModel()
        {
            //force the model state to have an error
            this.controller.ModelState.AddModelError("ErrorOld", "EmptyModel");
            var actual = await this.controller.Edit(null, null, false);

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
            var view = actual as ViewResult;
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task EditPostTestNoCategoryAttributes()
        {
            var model = new CategoryW() { Name = "Name" };
            var actual = await this.controller.Edit(model, null, false);

            int errors = 0;
            foreach (var state in this.controller.ModelState.Values)
            {
                errors += state.Errors.Count;
            }

            Assert.AreEqual(1, errors);

            //assert we got back a view
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
            var view = actual as ViewResult;
            AssertViewHasCategoryNames(view);
        }

        [TestMethod]
        public async Task EditPostTestWithValidModel()
        {
            var model = new CategoryW() { Name = "NameN", CategoryAttributes = new List<CategoryAttributeW>()
            {
                new CategoryAttributeW() { AttributeName = "AN" }
            }};
            var actual = await this.controller.Edit(model, null, false);

            workerMock.Verify(m => m.Edit(model, null, false));

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

        private void AssertViewHasCategoryNames(ViewResult view)
        {
            var hasParentIdList = view.ViewData.Keys.Contains("ParentId");
            Assert.IsTrue(hasParentIdList);

            var categoryNames = view.ViewData["ParentId"] as SelectList;
            Assert.IsNotNull(categoryNames);
        }
    }
}
