using ECommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji wiele-do-wielu (EF Core 5+)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",  // nazwa tabeli łączącej
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProductId");
                        j.ToTable("OrderProducts");
                    });
        }
    }

  
}
