using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        public IProductsRepository ProductsRepository { get; set; }

        public IOrderProcessor OrderProcessor { get; set; }

        public ActionResult AddToCart(Cart cart, long id, string returnUrl)
        {
            var product = ProductsRepository.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            var product = ProductsRepository.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Error: empty cart");
                return View(shippingDetails);
            }

            if (!ModelState.IsValid)
            {
                return View(shippingDetails);
            }

            OrderProcessor.ProcessOrder(cart, shippingDetails);
            cart.Clear();
            return View("Completed");
        }
    }
}