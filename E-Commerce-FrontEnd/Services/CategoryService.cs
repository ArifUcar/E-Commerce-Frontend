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

        public async Task<Category> GetCategoryById(Guid id)
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

        public async Task<bool> CreateCategory(CategoryCreateModel category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Categories/CreateCategory", new { categoryName = category.CategoryName });
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategori eklenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCategory(CategoryUpdateRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/Categories/UpdateCategory", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategori güncellenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Categories/DeleteCategory/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kategori silinirken hata oluştu: {ex.Message}");
                return false;
            }
        }
    }
} 