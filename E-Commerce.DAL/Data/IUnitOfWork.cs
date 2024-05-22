using E_Commerce.DAL.Repositories.CartItems;
using E_Commerce.DAL.Repositories.Carts;
using E_Commerce.DAL.Repositories.Category;
using E_Commerce.DAL.Repositories.Orders;
using E_Commerce.DAL.Repositories.Products;

namespace E_Commerce.DAL.Data;

public interface IUnitOfWork
{
    public ICartRepository CartRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ICartItemRepository CartItemRepository { get; }

    void SaveChanges();
}
