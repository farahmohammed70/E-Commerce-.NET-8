using E_Commerce.BL.Dtos;
using E_Commerce.DAL.Data.Models;
using E_Commerce.DAL.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace E_Commerce.BL.Managers.Cart;

public class CartManager : ICartManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }
    public CartDto GetCart()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            throw new ArgumentException("please login first!");
        }
        var cart = _unitOfWork.CartRepository.GetByUserId(GetUserId());
        if (cart == null)
        {
            throw new ArgumentException("Cart not found for the user");
        }

        var cartDto = new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = _unitOfWork.CartItemRepository.GetByCartId(cart.Id).Select(ci => new CartItemDto
            {
                Quantity = ci.Quantity,
                Price = ci.Price,
                ProductId = ci.ProductId,
            }).ToList()
        };

        return cartDto;
    }
    public void AddToCart(int productId, int quantity)
    {
        var product = _unitOfWork.ProductRepository.GetById(productId);
        if (product == null)
        {
            throw new ArgumentException($"Product with id {productId} not found");
        }

        var cart = _unitOfWork.CartRepository.GetByUserId(GetUserId());
        if (cart == null)
        {
            cart = new DAL.Data.Models.Cart { UserId = GetUserId() };
            _unitOfWork.CartRepository.Add(cart);
            _unitOfWork.SaveChanges();
        }


        var cartItem = _unitOfWork.CartItemRepository.GetByCartId(cart.Id).FirstOrDefault(item => item.ProductId == productId);
        if (cartItem != null)
        {

            throw new ArgumentException($"this product already exists");
        }

        cart.CartItems.Add(new CartItem
        {
            ProductId = productId,
            Quantity = quantity,
            Price = product.Price * quantity,
        });

        _unitOfWork.CartRepository.Update(cart);
        _unitOfWork.SaveChanges();
    }

    public void RemoveFromCart(int productId)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            throw new ArgumentException("please login first!");
        }

        var cart = _unitOfWork.CartRepository.GetByUserId(GetUserId());
        if (cart == null)
        {
            throw new ArgumentException("Cart not found for the user");
        }

        var cartItem = _unitOfWork.CartItemRepository.GetByCartId(cart.Id).FirstOrDefault(item => item.ProductId == productId);
        if (cartItem == null)
        {
            throw new ArgumentException($"Product with id {productId} not found in the cart");
        }

        cart.CartItems.Remove(cartItem);
        _unitOfWork.CartRepository.Update(cart);
        _unitOfWork.SaveChanges();
    }

    public void EditCartItemQuantity(int productId, int quantity)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            throw new ArgumentException("please login first!");
        }
        var product = _unitOfWork.ProductRepository.GetById(productId);
        var cart = _unitOfWork.CartRepository.GetByUserId(GetUserId());
        if (cart == null)
        {
            throw new ArgumentException("Cart not found for the user");
        }

        var cartItem = _unitOfWork.CartItemRepository.GetByCartId(cart.Id).FirstOrDefault(item => item.ProductId == productId);
        if (cartItem == null)
        {
            throw new ArgumentException($"Product with id {productId} not found in the cart");
        }
        cartItem.Price = product!.Price * quantity;
        _unitOfWork.CartRepository.Update(cart);
        _unitOfWork.SaveChanges();
    }
    private string GetUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }

    public void ClearCart()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            throw new ArgumentException("please login first!");
        }

        var cart = _unitOfWork.CartRepository.GetByUserId(GetUserId());
        if (cart == null)
        {
            throw new ArgumentException("Cart not found for the user");
        }
        _unitOfWork.CartRepository.Delete(cart);
        _unitOfWork.SaveChanges();

    }
}
