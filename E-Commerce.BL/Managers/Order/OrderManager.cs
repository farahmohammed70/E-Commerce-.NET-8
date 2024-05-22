using System.Security.Claims;
using E_Commerce.BL.Dtos;
using E_Commerce.DAL.Data;
using E_Commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.BL.Managers.Order;

public class OrderManager : IOrderManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public void PlaceOrder(PlaceOrderDto request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        string userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("PLease login first!!");
        }

        var orderItems = new List<OrderItem>();

        foreach (var item in request.OrderItems!)
        {
            var product = _unitOfWork.ProductRepository.GetById(item.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {item.ProductId} not found.");
            }

            orderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price * item.Quantity
            });
        }

        var order = new DAL.Data.Models.Order
        {
            UserId = userId,
            TotalPrice = CalculateTotalPrice(orderItems),
            OrderDate = DateTime.UtcNow,
            Items = orderItems
        };

        _unitOfWork.OrderRepository.PlaceOrder(order);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<OrderDto> GetOrderHistory()
    {
        string userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("Please login first!!!");
        }

        var orderHistory = _unitOfWork.OrderRepository.GetOrderHistory()
            .Where(o => o.UserId == userId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                TotalPrice = o.TotalPrice,
                CreatedDateTime = o.OrderDate
            })
            .ToList();

        return orderHistory;
    }

    private string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }


    private decimal CalculateTotalPrice(List<OrderItem> orderItems)
    {
        decimal totalPrice = 0;
        foreach (var orderItem in orderItems)
        {
            totalPrice += orderItem.Price;
        }
        return totalPrice;
    }
}
