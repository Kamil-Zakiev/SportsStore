namespace SportsStore.WebUI.Controllers
{
    using SportsStore.Domain.Abstract;
    using SportsStore.Domain.Entities;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class AdminController : Controller
    {
        public IProductsRepository ProductsRepository { get; set; }

        public ActionResult Index()
        {
            var allProducts = ProductsRepository.Products.OrderBy(p => p.Id).ToArray();
            return View(allProducts);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public RedirectToRouteResult Delete(long id)
        {
            var product = ProductsRepository.Products.Single(p => p.Id == id);
            ProductsRepository.Delete(product);
            TempData["message"] = $"{product.Name} was deleted";
            return RedirectToAction("Index");
        }

        public ViewResult Edit(long id)
        {
            var product = ProductsRepository.Products.SingleOrDefault(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    ProductsRepository.Save(product);
                    TempData["message"] = $"{product.Name} has been saved";
                    return RedirectToAction("Index");
                }

                // fix transient because of mvc binding
                // "a different object with the same identifier value was already associated with the session: 4, of entity: SportsStore.Domain.Entities.Product"
                var persistedProduct = ProductsRepository.Products.Single(p => p.Id == product.Id);
                persistedProduct.Name = product.Name;
                persistedProduct.Description = product.Description;
                persistedProduct.Category = product.Category;
                persistedProduct.Price = product.Price;
                
                ProductsRepository.Update(persistedProduct);
                TempData["message"] = $"{product.Name} has been updated";
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}