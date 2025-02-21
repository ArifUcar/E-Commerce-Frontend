using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

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
            return await _httpClient.GetFromJsonAsync<int>("api/Order/total-count");
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
            return await _httpClient.GetFromJsonAsync<decimal>("api/Order/total-revenue");
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
            return await _httpClient.GetFromJsonAsync<List<Order>>($"api/Order/recent/{count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Son siparişler alınırken hata: {ex.Message}");
            return new List<Order>();
        }
    }
}

// Auth verilerini deserialize etmek için kullanılacak model
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