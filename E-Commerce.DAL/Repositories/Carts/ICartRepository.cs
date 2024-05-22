using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Carts;

public interface ICartRepository : IGenericRepository<Cart>
{
    public void AddToCart(int productId, int quantity);
    public void RemoveFromCart(int productId);
    public void EditCartItemQuantity(int productId, int quantity);
    public Product GetByProductId(int productId);
    public Cart GetByUserId(string userId);
}
