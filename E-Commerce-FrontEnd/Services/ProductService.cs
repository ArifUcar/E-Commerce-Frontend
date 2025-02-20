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

        public async Task<int> GetTotalProductCount()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<int>("api/Product/GetCount");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }

        public async Task<List<Product>> GetLowStockProducts(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Product>>($"api/Product/GetLowStock/{count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Düşük stoklu ürünler alınırken hata oluştu: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<bool> AddProduct(ProductCreateModel product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Product", new
                {
                    productName = product.ProductName,
                    description = product.Description,
                    price = product.Price,
                    stockQuantity = product.StockQuantity,
                    categoryId = product.CategoryId,
                    base64Image = product.Base64Image,
                    color = product.Color,
                    size = product.Size,
                    material = product.Material,
                    brand = product.Brand,
                    model = product.Model,
                    warranty = product.Warranty,
                    specifications = product.Specifications,
                    additionalInformation = product.AdditionalInformation,
                    weight = product.Weight,
                    weightUnit = product.WeightUnit,
                    dimensions = product.Dimensions,
                    stockCode = product.StockCode,
                    barcode = product.Barcode
                });
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün eklenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateProduct(string id, ProductCreateModel product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", new
                {
                    productName = product.ProductName,
                    description = product.Description,
                    price = product.Price,
                    stockQuantity = product.StockQuantity,
                    categoryId = product.CategoryId,
                    base64Image = product.Base64Image,
                    color = product.Color,
                    size = product.Size,
                    material = product.Material,
                    brand = product.Brand,
                    model = product.Model,
                    warranty = product.Warranty,
                    specifications = product.Specifications,
                    additionalInformation = product.AdditionalInformation,
                    weight = product.Weight,
                    weightUnit = product.WeightUnit,
                    dimensions = product.Dimensions,
                    stockCode = product.StockCode,
                    barcode = product.Barcode
                });
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün güncellenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Product/Delete/{id}");
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