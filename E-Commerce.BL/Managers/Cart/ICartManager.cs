using E_Commerce.BL.Dtos;

namespace E_Commerce.BL.Managers.Cart;

public interface ICartManager
{
    void AddToCart(int productId, int quantity);
    void RemoveFromCart(int productId);
    void EditCartItemQuantity(int productId, int quantity);
    CartDto GetCart();
    void ClearCart();
}
