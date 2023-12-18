using InventoryDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InverntoryData
{
    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = InventoryDatabase"
            );
        }
    }
}