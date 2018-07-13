using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void CheckAddToCartLine()
        {
            // arrange
            var cart = new Cart();
            var product = new Product
            {
                Id = 1
            };

            // act
            cart.AddItem(product, 1);

            // assert
            Assert.AreEqual(cart.Lines.Count, 1);
            Assert.AreEqual(cart.Lines[0].Product, product);
        }

        [TestMethod]
        public void CheckAddSameToCartLine()
        {
            // arrange
            var cart = new Cart();
            var product = new Product
            {
                Id = 1
            };

            // act
            cart.AddItem(product, 1);
            cart.AddItem(product, 10);

            // assert
            Assert.AreEqual(cart.Lines.Count, 1);
            Assert.AreEqual(cart.Lines[0].Product, product);
            Assert.AreEqual(cart.Lines[0].Quantity, 11);
        }

        [TestMethod]
        public void CheckRemoveFromCartLine()
        {
            // arrange
            var cart = new Cart();
            var product1 = new Product { Id = 1 };
            var product2 = new Product { Id = 2 };

            // act
            cart.AddItem(product1, 1);
            cart.AddItem(product1, 10);
            cart.AddItem(product2, 10);

            cart.RemoveItem(product1);

            // assert
            Assert.AreEqual(cart.Lines.Count, 1);
            Assert.AreEqual(cart.Lines[0].Product, product2);
            Assert.AreEqual(cart.Lines[0].Quantity, 10);
        }

        [TestMethod]
        public void CheckTotalValue()
        {
            // arrange
            var cart = new Cart();
            var product1 = new Product { Id = 1, Price = 1 };
            var product2 = new Product { Id = 2, Price = 2 };

            // act
            cart.AddItem(product1, 10);
            cart.AddItem(product2, 10);

            // assert
            Assert.AreEqual(cart.ComputeTotalValue(), 10 + 10 * 2);
        }

        [TestMethod]
        public void CheckClearCart()
        {
            // arrange
            var cart = new Cart();
            var product1 = new Product { Id = 1, Price = 1 };
            var product2 = new Product { Id = 2, Price = 2 };

            // act
            cart.AddItem(product1, 10);
            cart.AddItem(product2, 10);

            cart.Clear();
            // assert
            Assert.AreEqual(cart.Lines.Count, 0);
        }

    }
}
