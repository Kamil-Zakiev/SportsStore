using SportsStore.Domain.Abstract;
using System;
using System.Linq;
using SportsStore.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace SportsStore.Domain.Concrete
{
    public class NHProductsRepository : IProductsRepository
    {
        public ISession Session { get; set; }

        public IQueryable<Product> Products => Session.Query<Product>();

        public void Delete(Product product)
        {
            using (var tr = Session.BeginTransaction())
            {
                Session.Delete(product);
                tr.Commit();
            }
        }

        public void Save(Product product)
        {
            if (product.Id != 0)
            {
                throw new InvalidOperationException("You should update product, not save");
            }

            using (var tr = Session.BeginTransaction())
            {
                Session.Save(product);
                tr.Commit();
            }
        }

        public void Update(Product product)
        {
            if (product.Id == 0)
            {
                throw new InvalidOperationException("You should save product, not update");
            }
            using (var tr = Session.BeginTransaction())
            {
                Session.Update(product);
                tr.Commit();
            }
        }
    }
}
