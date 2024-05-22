using E_Commerce.BL.Dtos;

namespace E_Commerce.BL.Managers.Product;

public interface IProductManager
{
    IEnumerable<ViewProductDto> GetProducts(string? category, string? name);
    public ProductDetailsDto? GetById(int id);
    public void AddProduct(ProductDto product);
    public void DeleteProduct(int id);
    public void UpdateProduct(int id, ProductDto product);
}
