using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    using Domain.Abstract;
    using SportsStore.WebUI.Models;
    using System.Linq;

    public class ProductController : Controller
    {
        public IProductsRepository ProductsRepository { get; set; }

        public int PageSize = 3;

        public ViewResult List(string category, int page = 1)
        {
            var pagedProducts = ProductsRepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var model = new ProductsListViewModel
            {
                Products = pagedProducts,
                CurrentCategory = category,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = ProductsRepository.Products.Where(p => category == null || p.Category == category).Count(),
                    ItemsPerPage = PageSize
                }
            };
            
            return View(model);
        }
    }
}