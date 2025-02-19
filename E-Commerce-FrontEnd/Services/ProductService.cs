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
    }
} 