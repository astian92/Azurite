using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Azurite.Store.Controllers;
using Azurite.Store.Workers.Contracts;
using Azurite.Store.Wrappers;

namespace Azurite.Store.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private Mock<ICategoryWorker> _mockWorker;

        private CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mockWorker = new Mock<ICategoryWorker>();
            _mockWorker.Setup(x => x.GetAll()).Returns(new List<CategoryW>()
            {
                new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици" },
                new CategoryW() { Id = Guid.NewGuid(), Name = "Пръстени", Description = "Сребърни пръстени" }
            }.AsQueryable());

            _mockWorker.Setup(x => x.GetCategory(It.IsAny<Guid>())).Returns(new CategoryW() { Id = Guid.NewGuid(), Name = "Обици", Description = "Златни обици" });

            _controller = new CategoriesController(_mockWorker.Object);
            var urlMock = new Mock<UrlHelper>();
            urlMock.Setup(m => m.Action(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RouteValueDictionary>()))
                .Returns<string, string, RouteValueDictionary>((a, c, r) => c + "/" + a);

            this._controller.Url = urlMock.Object;
        }
    }
}
