using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalOrderCount()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<int>("api/Orders/count");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sipariş sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetTotalRevenue()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<decimal>("api/Orders/revenue");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Toplam gelir alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }

        public async Task<List<Order>> GetRecentOrders(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Order>>($"api/Orders/recent/{count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Son siparişler alınırken hata oluştu: {ex.Message}");
                return new List<Order>();
            }
        }
    }
} 