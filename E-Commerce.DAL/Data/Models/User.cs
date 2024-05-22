using Microsoft.AspNetCore.Identity;
namespace E_Commerce.DAL.Data.Models;

public class User : IdentityUser
{
    public string Address { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string UserRole { get; set; } = "User";
    public ICollection<Order> Orders { get; set; } = [];
    public int CartID { get; set; }
    public Cart? Cart { get; set; }
}
