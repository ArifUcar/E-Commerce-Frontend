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
            if (_cartItems == null)
            {
                var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CART_KEY);
                _cartItems = string.IsNullOrEmpty(cartJson) 
                    ? new List<CartItem>() 
                    : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
            }
            return _cartItems;
        }

        public async Task AddToCart(Product product, int quantity = 1)
        {
            var currentProduct = await _productService.GetProductById(product.Id);
            if (currentProduct == null)
            {
                throw new Exception("Ürün bulunamadı.");
            }

            var items = await GetCartItems();
            var existingItem = items.FirstOrDefault(i => i.ProductId == product.Id);

            int totalRequestedQuantity = quantity;
            if (existingItem != null)
            {
                totalRequestedQuantity += existingItem.Quantity;
            }

            if (totalRequestedQuantity > currentProduct.StockQuantity)
            {
                throw new Exception($"Üzgünüz, bu üründen yalnızca {currentProduct.StockQuantity} adet stok kaldı.");
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                items.Add(new CartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ImageUrl = !string.IsNullOrEmpty(product.Base64Image) 
                        ? $"data:image/jpeg;base64,{product.Base64Image}" 
                        : product.ImagePath,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            await SaveCart();
        }

        public async Task UpdateQuantity(string cartItemId, int quantity)
        {
            var items = await GetCartItems();
            var item = items.FirstOrDefault(i => i.Id == cartItemId);
            
            if (item != null)
            {
                var currentProduct = await _productService.GetProductById(item.ProductId);
                if (currentProduct == null)
                {
                    throw new Exception("Ürün bulunamadı.");
                }

                if (quantity > currentProduct.StockQuantity)
                {
                    throw new Exception($"Üzgünüz, bu üründen yalnızca {currentProduct.StockQuantity} adet stok kaldı.");
                }

                if (quantity <= 0)
                {
                    items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                await SaveCart();
            }
        }

        public async Task RemoveFromCart(string cartItemId)
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

        private async Task SaveCart()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CART_KEY, 
                JsonSerializer.Serialize(_cartItems));
            OnChange?.Invoke();
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