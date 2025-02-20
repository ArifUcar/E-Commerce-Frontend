using E_Commerce_FrontEnd.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string FAVORITES_KEY = "favorites";
        private List<FavoriteItem> _favorites;

        public event Action OnChange;

        public FavoriteService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<List<FavoriteItem>> GetFavorites()
        {
            if (_favorites == null)
            {
                var favoritesJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", FAVORITES_KEY);
                _favorites = string.IsNullOrEmpty(favoritesJson)
                    ? new List<FavoriteItem>()
                    : JsonSerializer.Deserialize<List<FavoriteItem>>(favoritesJson);
            }
            return _favorites;
        }

        public async Task AddToFavorites(Product product)
        {
            var favorites = await GetFavorites();
            if (!favorites.Any(f => f.ProductId == product.Id))
            {
                favorites.Add(new FavoriteItem
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ImageUrl = !string.IsNullOrEmpty(product.Base64Image)
                        ? $"data:image/jpeg;base64,{product.Base64Image}"
                        : product.ImagePath,
                    Price = product.Price,
               
                });
                await SaveFavorites();
            }
            OnChange?.Invoke();
        }

        public async Task RemoveFromFavorites(string productId)
        {
            var favorites = await GetFavorites();
            var item = favorites.FirstOrDefault(f => f.ProductId == productId);
            if (item != null)
            {
                favorites.Remove(item);
                await SaveFavorites();
            }
            OnChange?.Invoke();
        }

        public async Task ClearFavorites()
        {
            _favorites = new List<FavoriteItem>();
            await SaveFavorites();
        }

        private async Task SaveFavorites()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", FAVORITES_KEY,
                JsonSerializer.Serialize(_favorites));
            OnChange?.Invoke();
        }

        public bool IsFavorite(string productId)
        {
            return _favorites?.Any(f => f.ProductId == productId) ?? false;
        }

        public int GetFavoriteCount()
        {
            return _favorites?.Count ?? 0;
        }
    }
} 