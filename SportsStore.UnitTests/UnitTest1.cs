using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using System.Linq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // arrange
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[] {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"},
                new Product { Id = 3, Name = "P3"},
                new Product { Id = 4, Name = "P4"},
                new Product { Id = 5, Name = "P5"}
            }.AsQueryable());

            var controller = new ProductController()
            {
                ProductsRepository = mockRepo.Object
            };

            controller.PageSize = 3;

            // act
            var result = ((ProductsListViewModel)controller.List(null, 2).Model).Products;

            //assert
            Assert.IsTrue(result.Length == 2);
            Assert.AreEqual(result[0].Name, "P4");
            Assert.AreEqual(result[1].Name, "P5");
        }

        [TestMethod]
        public void CheckPageLinks()
        {
            // arrange
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // act
            var mvcHtmlString = PagingHelpers.PageLinks(null, pagingInfo, i => "Page" + i);

            // assert
            Assert.AreEqual(mvcHtmlString.ToString(), @"<a href=""Page1"">1</a>"
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void CheckPagedViewModel()
        {
            // arrange
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[] {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"},
                new Product { Id = 3, Name = "P3"},
                new Product { Id = 4, Name = "P4"},
                new Product { Id = 5, Name = "P5"}
            }.AsQueryable());

            var controller = new ProductController()
            {
                ProductsRepository = mockRepo.Object
            };

            controller.PageSize = 3;

            // act
            var pagingInfo = ((ProductsListViewModel)controller.List(null, 2).Model).PagingInfo;

            // assert
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
        }

        [TestMethod]
        public void CheckFilterProducts()
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

            var controller = new ProductController()
            {
                ProductsRepository = mockRepo.Object
            };

            controller.PageSize = 3;

            // act
            var result = (ProductsListViewModel)controller.List("Category1", 1).Model;
            var products = result.Products;

            // assert
            Assert.AreEqual(products.Length, 3);
            Assert.IsTrue(products[0].Name == "P1" && products[0].Category == "Category1");
            Assert.IsTrue(products[1].Name == "P2" && products[1].Category == "Category1");
            Assert.IsTrue(products[2].Name == "P5" && products[2].Category == "Category1");
        }

        [TestMethod]
        public void CheckConcreteCategoryTotalCount()
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

            var controller = new ProductController()
            {
                ProductsRepository = mockRepo.Object
            };

            controller.PageSize = 3;

            // act
            var cat1Count = ((ProductsListViewModel)controller.List("Category1", 1).Model).PagingInfo.TotalItems;
            var cat2Count = ((ProductsListViewModel)controller.List("Category2", 1).Model).PagingInfo.TotalItems;
            var cat3Count = ((ProductsListViewModel)controller.List("Category3", 1).Model).PagingInfo.TotalItems;
            var nullCount = ((ProductsListViewModel)controller.List(null, 1).Model).PagingInfo.TotalItems;

            // assert
            Assert.AreEqual(cat1Count, 3);
            Assert.AreEqual(cat2Count, 1);
            Assert.AreEqual(cat3Count, 1);
            Assert.AreEqual(nullCount, 5);
        }
    }
}
