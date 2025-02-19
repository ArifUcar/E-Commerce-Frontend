using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(string id);
        Task<List<Product>> GetProductsByCategory(string categoryId);
        Task<List<Product>> SearchProducts(string searchTerm);
        // Diğer metotlar eklenebilir
    }
} 