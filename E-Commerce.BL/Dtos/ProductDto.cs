using Microsoft.AspNetCore.Http;

namespace E_Commerce.BL.Dtos;

public class ProductDto
{
    public IFormFile? ImageFile { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
}
