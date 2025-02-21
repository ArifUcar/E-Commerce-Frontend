using E_Commerce_FrontEnd.Models;
using E_Commerce_FrontEnd.Models.Commands;

namespace E_Commerce_FrontEnd.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByCategory(Guid categoryId);
        Task<List<Product>> SearchProducts(string searchTerm);
        Task<int> GetTotalProductCount();
        Task<int> GetTotalStockProductCount();
        Task<int> GetOutOfStockProductCount();
        Task<int> GetLowStockProducts();
        Task<List<Product>> GetDiscountedProducts();
        Task<bool> AddProduct(CreateProductCommand product);
        Task<bool> UpdateProduct(Guid id, CreateProductCommand product);
        Task<bool> DeleteProduct(Guid id);
        // Diğer metotlar eklenebilir
    }
} 