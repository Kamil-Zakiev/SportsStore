namespace SportsStore.Domain.Abstract
{
    using System.Linq;
    using Entities;

    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }

        void Save(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}