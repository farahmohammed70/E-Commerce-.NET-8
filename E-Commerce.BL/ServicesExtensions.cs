using E_Commerce.BL.Managers.Cart;
using E_Commerce.BL.Managers.Category;
using E_Commerce.BL.Managers.Order;
using E_Commerce.BL.Managers.Product;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.BL;

public static class ServicesExtensions
{
    public static void AddBLServices(this IServiceCollection services)
    {
        services.AddScoped<IProductManager, ProductManager>();
        services.AddScoped<ICartManager, CartManager>();
        services.AddScoped<IOrderManager, OrderManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
    }
}
