using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class User
    {
        private readonly Context _context;

        public User(Context context)
        {
            _context = context;
        }

        public async Task PlaceOrderAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.Quantity < quantity)
                throw new Exception("Not enough");

            product.Quantity -= quantity;

            var order = new Order
            {
                ProductId = productId,
                Quantity = quantity,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAvailableProductsAsync()
        {
            return await _context.Products
                .Where(p => p.Quantity > 0)
                .ToListAsync();
        }

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            return product;
        }
    }
}
