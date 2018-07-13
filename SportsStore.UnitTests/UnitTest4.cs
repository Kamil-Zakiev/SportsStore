using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Controllers;
using SportsStore.Domain.Abstract;
using Moq;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void CheckUserAddedAProduct()
        {
            // arrange
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(() => new[]
            {
                new Product { Id = 1 },
                new Product { Id = 2 },
                new Product { Id = 3 },
            }.AsQueryable());

            var cartController = new CartController()
            {
                ProductsRepository = mockRepo.Object
            };

            var cart = new Cart();

            // act
            cartController.AddToCart(cart, 1, "initial url");

            // assert
            Assert.AreEqual(cart.Lines.Count, 1);
            Assert.AreEqual(cart.Lines.Single().Product.Id, 1);
        }

        [TestMethod]
        public void CheckGoToViewAfterAdd()
        {
            // arrange
            var mockRepo = new Mock<IProductsRepository>();
            mockRepo.Setup(m => m.Products).Returns(() => new[]
            {
                new Product { Id = 1 },
                new Product { Id = 2 },
                new Product { Id = 3 },
            }.AsQueryable());

            var cartController = new CartController()
            {
                ProductsRepository = mockRepo.Object
            };

            var cart = new Cart();

            // act
            var result = (RedirectToRouteResult)cartController.AddToCart(cart, 1, "initial url");

            // assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "initial url");
        }

        [TestMethod]
        public void CheckCartViewOptions()
        {
            // arrange
            var cart = new Cart();
            var cartController = new CartController();

            // act
            var model = (CartIndexViewModel)cartController.Index(cart, "123").Model;

            // assert
            Assert.AreEqual(model.Cart, cart);
            Assert.AreEqual(model.ReturnUrl, "123");
        }
    }
}