using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(string id);
    }
} 