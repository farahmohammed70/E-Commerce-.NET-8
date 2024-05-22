using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.CartItems;

public interface ICartItemRepository : IGenericRepository<CartItem>
{
    public IEnumerable<CartItem> GetByCartId(int cartId);
}
