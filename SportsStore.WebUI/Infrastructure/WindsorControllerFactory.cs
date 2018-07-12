namespace SportsStore.WebUI.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Domain.Abstract;
    using SportsStore.WebUI.Controllers;
    using NHibernate;
    using SportsStore.Domain.Concrete;

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private IWindsorContainer _container;
        public WindsorControllerFactory(ISessionFactory sessionFactory)
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<ISessionFactory>().Instance(sessionFactory));
            _container.Register(Component.For<ISession>().UsingFactoryMethod(kernel =>
            {
                return sessionFactory.OpenSession();
            }).LifestylePerWebRequest());
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return (IController)_container.Resolve(controllerType);
        }

        private void AddBindings()
        {
            _container.Register(Component.For<IProductsRepository>().ImplementedBy<NHProductsRepository>().LifestylePerWebRequest());
            _container.Register(Component.For<ProductController>().ImplementedBy<ProductController>().LifestylePerWebRequest());
            _container.Register(Component.For<NavController>().ImplementedBy<NavController>().LifestylePerWebRequest());            
        }
    }
}