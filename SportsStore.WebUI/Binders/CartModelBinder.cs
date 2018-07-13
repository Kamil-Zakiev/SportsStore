using SportsStore.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            if (cart == null)
            {
                controllerContext.HttpContext.Session[sessionKey] = cart = new Cart();
            }

            return cart;
        }
    }
}