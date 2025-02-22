using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace E_Commerce_FrontEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public OrderService(HttpClient httpClient, IJSRuntime jsRuntime)
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

        public async Task<List<Order>> GetMyOrders()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Order>>("api/Order/my-orders");
                if (response != null)
                {
                    return response.OrderByDescending(o => o.OrderDate).ToList();
                }
                return new List<Order>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Siparişler getirilirken hata oluştu: {ex.Message}");
                return new List<Order>();
            }
        }
        public async Task<int> GetTotalOrderCount()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<OrderCountResponse>("api/Order/count");
                return response?.TotalOrders ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Toplam sipariş sayısı alınırken hata: {ex.Message}");
                return 0;
            }
        }

     


        public async Task<decimal> GetTotalRevenue()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<OrderRevunneResponse>("api/Order/total-revenue");
                return response?.TotalRevenue ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Toplam gelir alınırken hata: {ex.Message}");
                return 0;
            }
        }

        public async Task<List<Order>> GetRecentOrders(int count)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Order>>($"api/Order/recent/{count}");
                return response ?? new List<Order>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Son siparişler alınırken hata: {ex.Message}");
                return new List<Order>();
            }
        }

        public async Task<bool> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                await SetAuthHeader();
                
                // Sipariş detaylarını konsola yazdır
                Console.WriteLine("=== Sipariş Detayları ===");
                Console.WriteLine($"Sipariş Durumu: {request.OrderStatusId}");
                Console.WriteLine($"Toplam Tutar: {request.TotalAmount:C2}");
                Console.WriteLine($"Müşteri Adı: {request.CustomerName}");
                Console.WriteLine($"Telefon: {request.CustomerPhone}");
                Console.WriteLine($"Adres: {request.ShippingAddress}");
                Console.WriteLine($"Şehir: {request.City}");
                Console.WriteLine($"İlçe: {request.District}");
                Console.WriteLine($"Posta Kodu: {request.ZipCode}");
                
                Console.WriteLine("\n=== Ürün Detayları ===");
                foreach (var item in request.OrderDetails)
                {
                    Console.WriteLine($"Ürün ID: {item.ProductId}");
                    Console.WriteLine($"Ürün Adı: {item.ProductName}");
                    Console.WriteLine($"Adet: {item.Quantity}");
                    Console.WriteLine($"Birim Fiyat: {item.UnitPrice:C2}");
                    Console.WriteLine("-------------------");
                }

                var response = await _httpClient.PostAsJsonAsync("api/Order", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sipariş oluşturulurken hata: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Order>> GetOrders()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<Order>>("api/Order");
                if (response != null)
                {
                    return response.OrderByDescending(o => o.OrderDate).ToList();
                }
                return new List<Order>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Siparişler getirilirken hata oluştu: {ex.Message}");
                return new List<Order>();
            }
        }


        public async Task<bool> UpdateOrderStatus(Guid orderId, Guid statusId)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.PutAsJsonAsync($"api/Order/{orderId}/status", statusId);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sipariş durumu güncellenirken hata oluştu: {ex.Message}");
                return false;
            }
        }
    }

    // Sadece Auth ile ilgili sınıflar burada kalacak
    public class AuthData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserData User { get; set; }
    }

    public class UserData
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}