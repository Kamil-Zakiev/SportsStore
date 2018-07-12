using SportsStore.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        public IProductsRepository ProductsRepository { get; set; }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            var menu = ProductsRepository.Products
                .Select(p => p.Category)
                .OrderBy(cat => cat)
                .Distinct()
                .ToArray();
            return PartialView(menu);
        }
    }
}