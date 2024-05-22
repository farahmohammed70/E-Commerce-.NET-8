using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Orders;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(MyAppContext context) : base(context)
    {
    }

    public void PlaceOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }



    public IEnumerable<Order> GetOrderHistory()
    {
        return _dbContext.Orders;
    }
}
