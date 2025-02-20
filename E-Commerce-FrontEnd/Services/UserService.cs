using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalUserCount()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<int>("api/Users/count");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
    }
} 