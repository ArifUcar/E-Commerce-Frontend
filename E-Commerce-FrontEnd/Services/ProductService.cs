using E_Commerce_FrontEnd.Models;
using E_Commerce_FrontEnd.Models.Commands;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        private readonly IJSRuntime _jsRuntime;

        public ProductService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        private async Task SetAuthHeader()
        {
            try
            {
                var authData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "auth_data");
                if (!string.IsNullOrEmpty(authData))
                {
                    var authInfo = JsonSerializer.Deserialize<AuthData>(authData);
                    if (!string.IsNullOrEmpty(authInfo?.Token))
                    {
                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", authInfo.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token ayarlanırken hata oluştu: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/Product");
                return response ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürünler getirilirken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductById(Guid id)
        {
            try
            {
                await SetAuthHeader();
                return await _httpClient.GetFromJsonAsync<Product>($"api/Product/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün getirilirken hata oluştu: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Product>> GetDiscountedProducts()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/Product/GetDiscountedProducts");
                if (response != null)
                {
                    // Sadece aktif indirimi olan ürünleri filtrele
                    return response.Where(p => 
                        p.DiscountRate > 0 && 
                        p.DiscountStartDate <= DateTime.Now && 
                        p.DiscountEndDate >= DateTime.Now
                    ).ToList();
                }
                return new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"İndirimli ürünler getirilirken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }


        public async Task<List<Product>> GetProductsByCategory(Guid categoryId)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Product>>($"api/Product/GetByCategory/{categoryId}");
                return response ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategoriye ait ürünler getirilirken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<List<Product>> SearchProducts(string searchTerm)
        {
            try
            {
                await SetAuthHeader();
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllProducts();

                var allProducts = await GetAllProducts();
                return allProducts.Where(p => 
                    p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün araması yapılırken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }
      

        public async Task<int> GetTotalProductCount()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<ProductStokSummary>("api/Product/stock-summary");
                return response?.TotalProducts ?? 0;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
        public async Task<int> GetTotalStockProductCount()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<ProductStokSummary>("api/Product/stock-summary");
                return response?.TotalStockQuantity ?? 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Toplam ürün stok sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
        public async Task<int> GetOutOfStockProductCount()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<ProductStokSummary>("api/Product/stock-summary");
                return response?.OutOfStockProducts ?? 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tükenmiş ürün sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
        public async Task<int> GetLowStockProducts()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<ProductStokSummary>("api/Product/stock-summary");
                return response?.LowStockProducts ?? 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stook saysı az olan ürünleri alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }


        public async Task<bool> AddProduct(CreateProductCommand product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Product", product);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün eklenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Guid id, CreateProductCommand product)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", product);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün güncellenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.DeleteAsync($"api/Product/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün silinirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        
    }
} 