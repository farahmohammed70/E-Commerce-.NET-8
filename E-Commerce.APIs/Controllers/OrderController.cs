using E_Commerce.BL.Dtos;
using E_Commerce.BL.Managers.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderManager _orderManager;

    public OrderController(IOrderManager orderManager)
    {
        _orderManager = orderManager;
    }
    [Authorize]
    [HttpPost("place-order")]
    public ActionResult PlaceOrder([FromBody] PlaceOrderDto request)
    {
        try
        {
            _orderManager.PlaceOrder(request);
            return Ok("Done! Order place Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize]
    [HttpGet("order-history")]
    public ActionResult GetOrderHistory()
    {
        try
        {
            IEnumerable<OrderDto> orderHistory = _orderManager.GetOrderHistory();
            return Ok(orderHistory);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
