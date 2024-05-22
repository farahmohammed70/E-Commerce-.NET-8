namespace E_Commerce.DAL.Data.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    public decimal TotalPrice { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}
