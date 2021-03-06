﻿namespace SportsStore.WebUI.Infrastructure
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
    using System.Configuration;
    using SportsStore.WebUI.Infrastructure.Abstract;
    using SportsStore.WebUI.Infrastructure.Concrete;

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
            RegisterControllers();

            _container.Register(Component.For<IProductsRepository>().ImplementedBy<NHProductsRepository>().LifestylePerWebRequest());
            _container.Register(Component.For<IAuthProvider>().ImplementedBy<FormsAuthProvider>().LifestyleSingleton());

            _container.Register(Component.For<IOrderProcessor>().UsingFactoryMethod(k =>
            {
                var emailSettings = new EmailSettings
                {
                    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
                };
                return new EmailOrderProcessor(emailSettings);
            }).LifestyleSingleton());
        }

        private void RegisterControllers()
        {
            _container.Register(Component.For<ProductController>().ImplementedBy<ProductController>().LifestylePerWebRequest());
            _container.Register(Component.For<NavController>().ImplementedBy<NavController>().LifestylePerWebRequest());
            _container.Register(Component.For<AdminController>().ImplementedBy<AdminController>().LifestylePerWebRequest());
            _container.Register(Component.For<AccountController>().ImplementedBy<AccountController>().LifestylePerWebRequest());
            _container.Register(Component.For<CartController>().ImplementedBy<CartController>().LifestyleTransient());            
        }
    }
}