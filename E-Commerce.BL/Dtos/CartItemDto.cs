using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL.Dtos;

public class CartItemDto
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
}
