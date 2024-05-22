using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Products;

public interface IProductRepository : IGenericRepository<Product>
{
    IEnumerable<Product> GetProducts(string? category, string? name);
    Product? GetById(int id);
}
