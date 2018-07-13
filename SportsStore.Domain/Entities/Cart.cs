using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lines { get; set; } = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var line = lines.SingleOrDefault(l => l.Product.Id == product.Id);
            if (line != null)
            {
                line.Quantity += quantity;
                return;
            }

            lines.Add(new CartLine
            {
                Product = product,
                Quantity = quantity
            });
        }

        public void RemoveItem(Product product)
        {
            lines.RemoveAll(l => l.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lines.Sum(l => l.Product.Price * l.Quantity);
        }

        public void Clear()
        {
            lines.Clear();
        }

        public IReadOnlyList<CartLine> Lines => lines;
    }

    public class CartLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
