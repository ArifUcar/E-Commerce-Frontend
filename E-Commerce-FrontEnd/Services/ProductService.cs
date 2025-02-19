using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/Product/GetAll");
                return response ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürünler getirilirken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Product>($"api/Product/GetById/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün getirilirken hata oluştu: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryId)
        {
            try
            {
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
    }
} 