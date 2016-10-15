using Azurite.Infrastructure.Config;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Workers.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azurite.Storehouse.Tests.Workers
{
    [TestClass]
    public class CustomersWorkerTests
    {
        private Mock<IRepository<Customer>> repMock;

        private IRepository<Customer> rep
        {
            get
            {
                return this.repMock.Object;
            }
        }

        private CustomersWorker worker;

        public CustomersWorkerTests()
        {
            this.repMock = new Mock<IRepository<Customer>>();
            this.repMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new Customer() { FirstName = "Fn" });
            this.repMock.Setup(m => m.GetAll()).Returns(new List<Customer>()
            {
                new Customer() { FirstName = "Fn1" },
                new Customer() { FirstName = "Fn2" }
            }.AsQueryable());

            this.worker = new CustomersWorker(rep);
            AutoMapperTestingConfig.RegisterMappings("Azurite.Storehouse");
        }

        [TestMethod]
        public void GetTest()
        {
            var actual = worker.Get(Guid.NewGuid());

            this.repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            Assert.IsNotNull(actual);
            Assert.AreEqual("Fn", actual.FirstName);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var actual = worker.GetAll();

            this.repMock.Verify(m => m.GetAll());
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());
        }
    }
}
