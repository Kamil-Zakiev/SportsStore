using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace SportsStore.Domain.Concrete
{
    public class NHProductsRepository : IProductsRepository
    {
        public ISession Session { get; set; }
        public IQueryable<Product> Products => Session.Query<Product>();
    }
}
