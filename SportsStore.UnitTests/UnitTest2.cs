using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Controllers;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using Moq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void CheckCategoriesList()
        {
            // arrange
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[] {
                new Product { Id = 1, Name = "P1", Category = "Category1" },
                new Product { Id = 2, Name = "P2", Category = "Category1" },
                new Product { Id = 3, Name = "P3", Category = "Category2" },
                new Product { Id = 4, Name = "P4", Category = "Category3" },
                new Product { Id = 5, Name = "P5", Category = "Category1" }
            }.AsQueryable());

            var controller = new NavController()
            {
                ProductsRepository = mockRepo.Object
            };

            // act
            var result = (string[])controller.Menu().Model;

            // assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Category1");
            Assert.AreEqual(result[1], "Category2");
            Assert.AreEqual(result[2], "Category3");
        }

        [TestMethod]
        public void CheckIndicatesSelectedCategory()
        {
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[] {
                new Product { Id = 1, Name = "P1", Category = "Category1" },
                new Product { Id = 2, Name = "P2", Category = "Category1" },
                new Product { Id = 3, Name = "P3", Category = "Category2" },
                new Product { Id = 4, Name = "P4", Category = "Category3" },
                new Product { Id = 5, Name = "P5", Category = "Category1" }
            }.AsQueryable());

            var controller = new NavController()
            {
                ProductsRepository = mockRepo.Object
            };

            // act
            var result = controller.Menu("Category1").ViewBag.SelectedCategory;

            // assert
            Assert.AreEqual(result, "Category1");
        }
    }
}
