namespace SportsStore.Domain.Abstract
{
    using System.Linq;
    using Entities;

    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }
    }
}