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

        private CategoriesController controller;

        public CategoriesControllerTests()
        {
            mockWorker = new Mock<ICategoryWorker>();
            mockWorker.Setup(x => x.GetAll()).Returns(new List<CategoryW>()
            {
                new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици", NameEN = "Earings", DescriptionEN = "Golden Earings" },
                new CategoryW() { Id = Guid.NewGuid(), Name = "Пръстени", Description = "Сребърни пръстени", NameEN = "Rings", DescriptionEN = "Silver rings" }
            }.AsQueryable());

            mockWorker.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици", NameEN = "Earings", DescriptionEN = "Golden Earings" });

            controller = new CategoriesController(mockWorker.Object);
            var urlMock = new Mock<UrlHelper>();
            urlMock.Setup(m => m.Action(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>()))
                .Returns<string, string, RouteValueDictionary>((a, c, r) => c + "/" + a);

            this.controller.Url = urlMock.Object;
        }
    }
}
