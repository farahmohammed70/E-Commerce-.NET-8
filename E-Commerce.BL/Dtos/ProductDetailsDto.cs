namespace E_Commerce.BL.Dtos;

public class ProductDetailsDto
{
    public string? ImageFile { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
}
