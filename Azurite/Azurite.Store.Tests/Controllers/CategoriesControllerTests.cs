using Azurite.Store.Controllers;
using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Azurite.Store.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private Mock<ICategoryWorker> mockWorker;

        private ICategoryWorker worker
        {
            get
            {
                return this.mockWorker.Object;
            }
        }

        private CategoriesController controller;

        public CategoriesControllerTests()
        {
            mockWorker = new Mock<ICategoryWorker>();
            var catList = new List<CategoryW>()
            {
                new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици" },
                new CategoryW() { Id = Guid.NewGuid(), Name = "Пръстени", Description = "Сребърни пръстени" }
            };

            mockWorker.Setup(x => x.GetAll()).Returns(catList.AsQueryable());
            mockWorker.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици" });

            controller = new CategoriesController(worker);
            var urlMock = new Mock<UrlHelper>();
            urlMock.Setup(m => m.Action(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>()))
                .Returns<string, string, RouteValueDictionary>((a, c, r) => c + "/" + a);

            this.controller.Url = urlMock.Object;
        }

        [TestMethod]
        public void IndexTest()
        {
            var actual = this.controller.Index(Guid.NewGuid());

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ViewResult));
        }
    }
}
