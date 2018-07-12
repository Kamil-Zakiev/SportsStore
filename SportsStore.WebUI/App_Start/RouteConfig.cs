using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new
                {
                    Controller = "Product",
                    action = "List",
                    category = default(string),
                    page = 1
                });

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new
                {
                    Controller = "Product",
                    action = "List",
                    category = default(string)
                }
                //, constraints: new { page = @"\d+" }
                );

            routes.MapRoute(
                name: null,
                url: "{category}/Page{page}",
                defaults: new
                {
                    Controller = "Product",
                    action = "List"
                }
                //, constraints: new { page = @"\d+" }
                );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}"
            );
        }
    }
}
