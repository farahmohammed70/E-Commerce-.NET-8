using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.CartItems;

public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(MyAppContext context) : base(context)
    {
    }

    public IEnumerable<CartItem> GetByCartId(int cartId)
    {
        return _dbContext.CartItems.Where(c => c.CartId == cartId).ToList();
    }
}
