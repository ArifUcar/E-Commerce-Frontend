using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface ICartService
    {
        event Action OnChange;
        Task<List<CartItem>> GetCartItems();
        Task AddToCart(Product product, int quantity = 1);
        Task UpdateQuantity(string cartItemId, int quantity);
        Task RemoveFromCart(string cartItemId);
        Task ClearCart();
        int GetCartItemCount();
        decimal GetCartTotal();
    }
} 