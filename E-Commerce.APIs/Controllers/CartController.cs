using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.BL.Managers.Cart;

namespace E_Commerce.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartManager _cartManager;

    public CartController(ICartManager cartManager)
    {
        _cartManager = cartManager;
    }

    [HttpGet]
    public ActionResult GetCart()
    {
        try
        {
            var cart = _cartManager.GetCart();
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [Authorize]
    [HttpPost("{productId}/{quantity}")]
    public ActionResult AddToCart(int productId, int quantity)
    {
        try
        {
            _cartManager.AddToCart(productId, quantity);
            return Ok("Item added to cart successfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Error adding item to cart: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpDelete("{productId}")]
    [Authorize]
    public ActionResult RemoveFromCart(int productId)
    {
        try
        {
            _cartManager.RemoveFromCart(productId);
            return Ok($"Product with ID {productId} removed from cart");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Error removing item from cart: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{productId}/{quantity}")]
    [Authorize]
    public ActionResult EditCartItemQuantity(int productId, int quantity)
    {
        try
        {
            _cartManager.EditCartItemQuantity(productId, quantity);
            return Ok($"Quantity of product with ID {productId} in cart updated successfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Error editing item quantity in cart: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpDelete]
    [Authorize]
    public ActionResult ClearCart()
    {
        try
        {
            _cartManager.ClearCart();
            return Ok($"Done! Clear cart ");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Error : {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
