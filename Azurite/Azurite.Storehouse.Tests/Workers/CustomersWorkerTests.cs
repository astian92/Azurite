using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Azurite.Infrastructure.Config;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Workers.Implementations;

namespace Azurite.Storehouse.Tests.Workers
{
    [TestClass]
    public class CustomersWorkerTests
    {
        private CustomersWorker _worker;

        private Mock<IRepository<Customer>> _repMock;

        public CustomersWorkerTests()
        {
            this._repMock = new Mock<IRepository<Customer>>();
            this._repMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new Customer() { FirstName = "Fn" });
            this._repMock.Setup(m => m.GetAll()).Returns(new List<Customer>()
            {
                new Customer() { FirstName = "Fn1" },
                new Customer() { FirstName = "Fn2" }
            }.AsQueryable());

            this._worker = new CustomersWorker(Rep);
            AutoMapperTestingConfig.RegisterMappings("Azurite.Storehouse");
        }

        private IRepository<Customer> Rep
        {
            get
            {
                return this._repMock.Object;
            }
        }

        [TestMethod]
        public void GetTest()
        {
            var actual = _worker.Get(Guid.NewGuid());

            this._repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            Assert.IsNotNull(actual);
            Assert.AreEqual("Fn", actual.FirstName);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var actual = _worker.GetAll();

            this._repMock.Verify(m => m.GetAll());
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());
        }
    }
}
