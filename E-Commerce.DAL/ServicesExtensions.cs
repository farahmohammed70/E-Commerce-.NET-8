using E_Commerce.DAL.Data;
using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Repositories.CartItems;
using E_Commerce.DAL.Repositories.Carts;
using E_Commerce.DAL.Repositories.Category;
using E_Commerce.DAL.Repositories.Orders;
using E_Commerce.DAL.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.DAL;

public static class ServicesExtensions
{
    public static void AddDALServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ECommerceDb");
        services.AddDbContext<MyAppContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
