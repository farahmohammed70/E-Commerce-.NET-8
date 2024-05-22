using E_Commerce.BL.Dtos;

namespace E_Commerce.BL.Managers.Order;

public interface IOrderManager
{
    void PlaceOrder(PlaceOrderDto request);
    IEnumerable<OrderDto> GetOrderHistory();

}
