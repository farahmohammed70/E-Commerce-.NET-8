using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
