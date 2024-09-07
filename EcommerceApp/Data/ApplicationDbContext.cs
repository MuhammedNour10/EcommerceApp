using EcommerceApp.Model;
using EcommerceApp.Model.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EcommerceApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(7,2)");
        var categories = new List<Category>()
        {
            new Category() { Id = 1, Name = "Dessert" }, 
            new Category() { Id = 2, Name = "Drinks" },
            new Category() { Id = 3, Name = "Pastries" }
        };
        builder.Entity<Category>().HasData(categories);
        
    }
    
}
