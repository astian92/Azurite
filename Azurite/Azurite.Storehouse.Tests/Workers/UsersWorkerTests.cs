using Azurite.Infrastructure.Config;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Workers.Implementations;
using Azurite.Storehouse.Wrappers;
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
    public class UsersWorkerTests
    {
        private Mock<IRepository<User>> repMock;

        private IRepository<User> rep
        {
            get
            {
                return this.repMock.Object;
            }
        }

        private UsersWorker worker;

        public UsersWorkerTests()
        {
            this.repMock = new Mock<IRepository<User>>();
            this.repMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new User() { Username = "Un" });
            this.repMock.Setup(m => m.GetAll()).Returns(new List<User>()
            {
                new User() { Username = "Un1", FirstName = "Fn1" },
                new User() { Username = "Un2", FirstName = "Fn2" },
                new User() { Username = "master", FirstName = "Master" },
                new User() { Username = "Un3", FirstName = "Fn3" }
            }.AsQueryable());

            this.worker = new UsersWorker(this.rep);
            AutoMapperTestingConfig.RegisterMappings("Azurite.Storehouse");
        }

        [TestMethod]
        public void GetTest()
        {
            var actual = this.worker.Get(Guid.NewGuid());

            repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            Assert.IsNotNull(actual);
            Assert.AreEqual("Un", actual.Username);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var actual = this.worker.GetAll();

            repMock.Verify(m => m.GetAll());
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddTestNull()
        {
            this.worker.Add(null);
        }

        [TestMethod]
        public void AddTest()
        {
            var userW = new UserW() { Username = "USN" };
            this.worker.Add(userW);

            this.repMock.Verify(m => m.Add(It.IsAny<User>()));
            this.repMock.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void EditTestNull()
        {
            this.worker.Edit(null);
        }

        [TestMethod]
        public void EditTest()
        {
            var userW = new UserW() { Username = "USN" };
            this.worker.Edit(userW);

            this.repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            this.repMock.Verify(m => m.Save());
        }

        [TestMethod]
        public void DeleteTest()
        {
            var actual = this.worker.Delete(Guid.NewGuid());
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ITicket));
            Assert.IsTrue(actual.IsOK);

            this.repMock.Verify(m => m.Remove(It.IsAny<Guid>()));
            this.repMock.Verify(m => m.Save());
        }
    }
}
