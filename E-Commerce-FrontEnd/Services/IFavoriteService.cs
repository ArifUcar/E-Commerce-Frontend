using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface IFavoriteService
    {
        event Action OnChange;
        Task<List<FavoriteItem>> GetFavorites();
        Task AddToFavorites(Product product);
        Task RemoveFromFavorites(string productId);
        Task ClearFavorites();
        bool IsFavorite(string productId);
        int GetFavoriteCount();
    }
} 