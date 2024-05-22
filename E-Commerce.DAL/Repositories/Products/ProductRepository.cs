using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL.Repositories.Products;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(MyAppContext context) : base(context)
    {
    }

    public IEnumerable<Product> GetProducts(string? category, string? name)
    {
        var query = _dbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category!.Name == category);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        return query.ToList();
    }

    public Product? GetById(int id)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Id == id);
    }
}
