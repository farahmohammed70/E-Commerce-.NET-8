using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Repositories.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Commerce.DAL.Repositories.Carts;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartRepository(MyAppContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Cart GetByUserId(string userId)
    {
        return _dbContext.Carts.FirstOrDefault(c => c.UserId == userId)!;
    }

    public void AddToCart(int productId, int quantity)
    {
        var product = _dbContext.Products.Find(productId);
        if (product == null)
        {
            throw new ArgumentException($"Product with id {productId} not found");
        }

        var cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == GetUserId());
        {
            cart = new Cart { UserId = GetUserId() };
            _dbContext.Carts.Add(cart);
        }

        var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                Price = product.Price,
            });
        }

        _dbContext.SaveChanges();
    }

    public void RemoveFromCart(int productId)
    {
        var cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == GetUserId());
        if (cart == null)
        {
            throw new ArgumentException("Cart is not found for this user");
        }

        var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
        if (cartItem == null)
        {
            throw new ArgumentException($"Product with id {productId} is not found in this cart");
        }

        cart.CartItems.Remove(cartItem);
        _dbContext.SaveChanges();
    }

    public void EditCartItemQuantity(int productId, int quantity)
    {
        var cart = _dbContext.Carts.FirstOrDefault(c => c.UserId == GetUserId());
        var cartItem = cart?.CartItems.FirstOrDefault(item => item.ProductId == productId);
        if (cartItem == null)
        {
            throw new ArgumentException($"Product with id {productId} not found in the cart");
        }

        cartItem.Quantity = quantity;
        _dbContext.SaveChanges();
    }

    public Product GetByProductId(int productId)
    {
        return _dbContext.Products.Find(productId)!;
    }
    private string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }
}
