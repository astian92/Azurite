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
    public class CategoriesWorkerTests
    {
        //private Mock<IRepository<Category>> repMock;

        //private IRepository<Category> rep
        //{
        //    get
        //    {
        //        return this.repMock.Object;
        //    }
        //}

        //private Mock<IRepository<CategoryAttribute>> attrRepMock;

        //private IRepository<CategoryAttribute> attrRep
        //{
        //    get
        //    {
        //        return this.attrRepMock.Object;
        //    }
        //}

        //private CategoriesWorker worker;

        //public CategoriesWorkerTests()
        //{
        //    this.repMock = new Mock<IRepository<Category>>();
        //    this.repMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(new Category() { Name = "Nm" });
        //    this.repMock.Setup(m => m.GetAll()).Returns(new List<Category>()
        //    {
        //        new Category() { Name = "Cat1" },
        //        new Category() { Name = "Cat2" },
        //        new Category() { Name = "Cat3" }
        //    }.AsQueryable());

        //    this.attrRepMock = new Mock<IRepository<CategoryAttribute>>();

        //    this.worker = new CategoriesWorker(this.rep, this.attrRep);
        //    AutoMapperTestingConfig.RegisterMappings("Azurite.Storehouse");
        //}

        //[TestMethod]
        //public void GetTest()
        //{
        //    var id = Guid.NewGuid();
        //    var actual = this.worker.Get(id);

        //    repMock.Verify(m => m.Get(id));
        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual("Nm", actual.Name);
        //}

        //[TestMethod]
        //public void GetAllTest()
        //{
        //    var actual = this.worker.GetAll();

        //    repMock.Verify(m => m.GetAll());
        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(3, actual.Count());
        //}

        //[TestMethod]
        //public void GetAllWithoutParentsTest()
        //{
        //    var actual = this.worker.GetAllWithoutParents();

        //    repMock.Verify(m => m.GetAll());
        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(3, actual.Count());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void AddTestNull()
        //{
        //    this.worker.Add(null);
        //}

        //[TestMethod]
        //public void AddTest()
        //{
        //    var catW = new CategoryW() { Name = "Cat4" };
        //    this.worker.Add(catW);

        //    this.repMock.Verify(m => m.Add(It.IsAny<Category>()));
        //    this.repMock.Verify(m => m.Save());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void EditTestNull()
        //{
        //    this.worker.Edit(null);
        //}

        //[TestMethod]
        //public void EditTestSimpleCase()
        //{
        //    var catW = new CategoryW() { Name = "Cat" };
        //    this.worker.Edit(catW);

        //    this.repMock.Verify(m => m.Get(It.IsAny<Guid>()));
        //    this.repMock.Verify(m => m.Save());
        //}

        //[TestMethod]
        //public void DeleteTest()
        //{
        //    var actual = this.worker.Delete(Guid.NewGuid());
        //    Assert.IsNotNull(actual);
        //    Assert.IsInstanceOfType(actual, typeof(ITicket));
        //    Assert.IsTrue(actual.IsOK);

        //    this.repMock.Verify(m => m.Remove(It.IsAny<Guid>()));
        //    this.repMock.Verify(m => m.Save());
        //}
    }
}
