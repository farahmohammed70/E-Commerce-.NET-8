using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;

namespace E_Commerce.DAL.Repositories.Orders;

public interface IOrderRepository : IGenericRepository<Order>
{
    public void PlaceOrder(Order order);
    IEnumerable<Order> GetOrderHistory();
}
