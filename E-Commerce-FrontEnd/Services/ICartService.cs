using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface ICartService
    {
        event Action OnChange;
        Task<List<CartItem>> GetCartItems();
        Task AddToCart(Product product, int quantity = 1);
        Task UpdateQuantity(Guid cartItemId, int quantity);
        Task RemoveFromCart(Guid cartItemId);
        Task ClearCart();
        int GetCartItemCount();
        decimal GetCartTotal();
    }
} 