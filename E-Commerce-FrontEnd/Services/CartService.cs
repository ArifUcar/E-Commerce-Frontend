using E_Commerce_FrontEnd.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class CartService : ICartService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IProductService _productService;
        private const string CART_KEY = "shopping_cart";
        private List<CartItem> _cartItems;

        public event Action OnChange;

        public CartService(IJSRuntime jsRuntime, IProductService productService)
        {
            _jsRuntime = jsRuntime;
            _productService = productService;
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            try
            {
                var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CART_KEY);
                
                if (!string.IsNullOrEmpty(cartJson))
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Büyük/küçük harf duyarlılığını kaldır
                    };
                    
                    _cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson, options);
                }
                
                _cartItems ??= new List<CartItem>();
                return _cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sepet yüklenirken hata oluştu: {ex.Message}");
                _cartItems = new List<CartItem>();
                return _cartItems;
            }
        }

        private async Task SaveCart()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                var cartJson = JsonSerializer.Serialize(_cartItems, options);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CART_KEY, cartJson);
                OnChange?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sepet kaydedilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task AddToCart(Product product, int quantity = 1)
        {
            var items = await GetCartItems();
            var existingItem = items.FirstOrDefault(i => i.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                items.Add(new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    Base64Image = product.Base64Image,
                    ImagePath = product.ImagePath,
                    Price = product.IsDiscounted ? product.CurrentPrice : product.Price,
                    Quantity = quantity
                });
            }

            await SaveCart();
        }

        public async Task UpdateQuantity(Guid cartItemId, int quantity)
        {
            var items = await GetCartItems();
            var item = items.FirstOrDefault(i => i.Id == cartItemId);
            
            if (item != null)
            {
                item.Quantity = quantity;
                await SaveCart();
            }
        }

        public async Task RemoveFromCart(Guid cartItemId)
        {
            var items = await GetCartItems();
            var item = items.FirstOrDefault(i => i.Id == cartItemId);
            
            if (item != null)
            {
                items.Remove(item);
                await SaveCart();
            }
        }

        public async Task ClearCart()
        {
            _cartItems = new List<CartItem>();
            await SaveCart();
        }

        public int GetCartItemCount()
        {
            return _cartItems?.Sum(i => i.Quantity) ?? 0;
        }

        public decimal GetCartTotal()
        {
            return _cartItems?.Sum(i => i.TotalPrice) ?? 0;
        }
    }
} 