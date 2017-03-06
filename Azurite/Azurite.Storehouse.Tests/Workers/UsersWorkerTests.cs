using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Azurite.Infrastructure.Config;
using Azurite.Infrastructure.Data.Contracts;
using Azurite.Infrastructure.ResponseHandling;
using Azurite.Storehouse.Data;
using Azurite.Storehouse.Workers.Implementations;
using Azurite.Storehouse.Wrappers;

namespace Azurite.Storehouse.Tests.Workers
{
    [TestClass]
    public class UsersWorkerTests
    {
        private UsersWorker _worker;

        private Mock<IRepository<User>> _repMock;

        public UsersWorkerTests()
        {
            this._repMock = new Mock<IRepository<User>>();
            this._repMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new User() { Username = "Un" });
            this._repMock.Setup(m => m.GetAll()).Returns(new List<User>()
            {
                new User() { Username = "Un1", FirstName = "Fn1" },
                new User() { Username = "Un2", FirstName = "Fn2" },
                new User() { Username = "master", FirstName = "Master" },
                new User() { Username = "Un3", FirstName = "Fn3" }
            }.AsQueryable());

            this._worker = new UsersWorker(this.Rep);
            AutoMapperTestingConfig.RegisterMappings("Azurite.Storehouse");
        }

        private IRepository<User> Rep
        {
            get
            {
                return this._repMock.Object;
            }
        }

        [TestMethod]
        public void GetTest()
        {
            var actual = this._worker.Get(Guid.NewGuid());

            _repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            Assert.IsNotNull(actual);
            Assert.AreEqual("Un", actual.Username);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var actual = this._worker.GetAll();

            _repMock.Verify(m => m.GetAll());
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddTestNull()
        {
            this._worker.Add(null);
        }

        [TestMethod]
        public void AddTest()
        {
            var userW = new UserW() { Username = "USN" };
            this._worker.Add(userW);

            this._repMock.Verify(m => m.Add(It.IsAny<User>()));
            this._repMock.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void EditTestNull()
        {
            this._worker.Edit(null);
        }

        [TestMethod]
        public void EditTest()
        {
            var userW = new UserW() { Username = "USN" };
            this._worker.Edit(userW);

            this._repMock.Verify(m => m.Get(It.IsAny<Guid>()));
            this._repMock.Verify(m => m.Save());
        }

        [TestMethod]
        public void DeleteTest()
        {
            var actual = this._worker.Delete(Guid.NewGuid());
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ITicket));
            Assert.IsTrue(actual.IsOK);

            this._repMock.Verify(m => m.Remove(It.IsAny<Guid>()));
            this._repMock.Verify(m => m.Save());
        }
    }
}
