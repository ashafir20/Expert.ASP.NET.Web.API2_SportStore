using System.Data.Entity;

namespace SportStore.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
            : base("name=SportStoreDb")
        {
            Database.SetInitializer<ProductDbContext>(new ProductDbInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}