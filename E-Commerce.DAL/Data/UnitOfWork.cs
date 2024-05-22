using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Repositories.CartItems;
using E_Commerce.DAL.Repositories.Carts;
using E_Commerce.DAL.Repositories.Category;
using E_Commerce.DAL.Repositories.Orders;
using E_Commerce.DAL.Repositories.Products;

namespace E_Commerce.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyAppContext _dbContext;

        public ICartRepository CartRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartItemRepository CartItemRepository { get; }

        public UnitOfWork(IProductRepository productRepository,
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            ICategoryRepository categoryRepository,
            ICartItemRepository cartItemRepository,
            MyAppContext context)
        {
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            CartRepository = cartRepository;
            _dbContext = context;
            CategoryRepository = categoryRepository;
            CartItemRepository = cartItemRepository;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
