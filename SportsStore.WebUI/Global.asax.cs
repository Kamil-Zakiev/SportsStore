namespace SportsStore.WebUI
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Infrastructure;
    using NHibernateConfig;
    using SportsStore.WebUI.Binders;
    using SportsStore.Domain.Entities;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var sessionFactory = NHibernateConfiguration.CreateFactory();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(sessionFactory));
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}