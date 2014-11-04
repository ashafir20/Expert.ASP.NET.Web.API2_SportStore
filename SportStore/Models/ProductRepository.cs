using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ProductRepository : IRepository
    {
        private ProductDbContext _context = new ProductDbContext();

        public IEnumerable<Product> Products
        {
            get { return _context.Products; }
        }

        public async Task<int> SaveProductAsync(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<Product> DeleteProductAsync(int productID)
        {
            Product dbEntry = _context.Products.Find(productID);
            if (dbEntry != null)
            {
                _context.Products.Remove(dbEntry);
            }

            await _context.SaveChangesAsync();
            return dbEntry;
        }

        public IEnumerable<Order> Orders
        {
            get { return _context.Orders.Include("Lines").Include("Lines.Product"); }
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            if (order.Id == 0)
            {
                _context.Orders.Add(order);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<Order> DeleteOrderAsync(int orderID)
        {
            Order dbEntry = _context.Orders.Find(orderID);
            if (dbEntry != null)
            {
                _context.Orders.Remove(dbEntry);
            }

            await _context.SaveChangesAsync();
            return dbEntry;
        }
    }
}