using E_Commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_Commerce.DAL.Data.Context;

public class MyAppContext : IdentityDbContext<User>
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
                    .HasOne(u => u.Cart)
                    .WithOne(c => c.User)
                    .HasForeignKey<Cart>(c => c.UserId);

        var product = modelBuilder.Entity<Product>();
        product.Property(p => p.Name).IsRequired();
        product.Property(p => p.Price).HasColumnType("decimal(10,2)");
        product.Property(p => p.CategoryId).IsRequired();

        product.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);

        // Data Seeding
        var categories = new List<Category>
    {
        new Category { Id = 1, Name = "Electronics", Description = "Electronics category description" },
        new Category { Id = 2, Name = "Laptops", Description = "Laptops category description" }
    };

        modelBuilder.Entity<Category>().HasData(categories);

        var products = new List<Product>
    {
        new Product { Id = 1, Name = "SAMSUNG Galaxy S24 Ultra", Description = "AI Android Smartphone, 512GB Storage, 12GB RAM, 200MP Camera, S Pen, Long Battery Life - Titanium Gray", Price = 66666, CategoryId = 1 },
        new Product { Id = 2, Name = "Apple 2022 MacBook", Description = "Apple 2022 MacBook Air laptop with M2 chip: 13.6-inch Liquid Retina display, 8GB RAM, 256GB SSD storage, 1080p FaceTime HD camera. Works with iPhone and iPad; Space Grey;", Price = 59750, CategoryId = 2 }
    };

        modelBuilder.Entity<Product>().HasData(products);

        var users = new List<User>
    {
        new User { Id = "1", UserName = "user1", Email = "user1@example.com", Address = "123 Street, City", Gender = Gender.Male },
        new User { Id = "2", UserName = "user2", Email = "user2@example.com", Address = "456 Avenue, Town", Gender = Gender.Female }
    };

        modelBuilder.Entity<User>().HasData(users);

        var carts = new List<Cart>
    {
        new Cart { Id = 1, UserId = "1" },
        new Cart { Id = 2, UserId = "2" }
    };

        modelBuilder.Entity<Cart>().HasData(carts);

        var cartItems = new List<CartItem>
    {
        new CartItem { Id = 1, Quantity = 1, Price = 66666, CartId = 1, ProductId = 1 },
        new CartItem { Id = 2, Quantity = 1, Price = 59750, CartId = 1, ProductId = 2 },
        new CartItem { Id = 3, Quantity = 1, Price = 66666, CartId = 2, ProductId = 1 },
        new CartItem { Id = 4, Quantity = 1, Price = 59750, CartId = 2, ProductId = 2 }
    };

        modelBuilder.Entity<CartItem>().HasData(cartItems);

        var orders = new List<Order>
    {
        new Order { Id = 1, TotalPrice = 2500, OrderDate = DateTime.Now, UserId = "1" },
        new Order { Id = 2, TotalPrice = 3500, OrderDate = DateTime.Now, UserId = "2" }
    };

        modelBuilder.Entity<Order>().HasData(orders);

        var orderItems = new List<OrderItem>
    {
        new OrderItem { Id = 1, Price = 66666, OrderId = 1, ProductId = 1 },
        new OrderItem { Id = 2, Price = 59750, OrderId = 2, ProductId = 2 }
    };

        modelBuilder.Entity<OrderItem>().HasData(orderItems);

        var roles = new List<IdentityRole>
    {
        new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
    };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }



    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
}
