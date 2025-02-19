using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Category>>("api/Categories/GetAllCategory");
                return response ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategoriler getirilirken hata oluştu: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<Category> GetCategoryById(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Category>($"api/Categories/GetById/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategori getirilirken hata oluştu: {ex.Message}");
                return null;
            }
        }
    }
} 