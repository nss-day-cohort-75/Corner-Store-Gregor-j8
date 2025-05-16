using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
public class CornerStoreDbContext : DbContext
{
    public DbSet<Cashier> Cashier { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> OrderProduct { get; set; }
    public DbSet<Product> Product { get; set; }
    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category { Id = 1, CategoryName = "Electronics" },
            new Category { Id = 2, CategoryName = "Groceries" },
            new Category { Id = 3, CategoryName = "Clothing" }
        });

        modelBuilder.Entity<Cashier>().HasData(new Cashier[]
        {
            new Cashier { Id = 1, FirstName = "Alice", LastName = "Johnson" },
            new Cashier { Id = 2, FirstName = "Bob", LastName = "Smith" }
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
            new Product { Id = 1, ProductName = "Laptop", Price = 1200.00M, Brand = "TechBrand", CategoryId = 1 },
            new Product { Id = 2, ProductName = "Apple", Price = 0.50M, Brand = "FreshFarms", CategoryId = 2 },
            new Product { Id = 3, ProductName = "Jeans", Price = 40.00M, Brand = "DenimCo", CategoryId = 3 }
        });
        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, CashierId = 1, PaidOnDate = DateTime.Now.AddDays(-1) },
            new Order { Id = 2, CashierId = 2, PaidOnDate = null }  // Not paid yet
        });

        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            new OrderProduct { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
            new OrderProduct { Id = 2, OrderId = 1, ProductId = 2, Quantity = 5 },
            new OrderProduct { Id = 3, OrderId = 2, ProductId = 3, Quantity = 2 }
        });
    }
}