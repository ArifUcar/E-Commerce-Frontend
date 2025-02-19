using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private bool _isAuthenticated;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _isAuthenticated = false;
        }

        public bool IsAuthenticated => _isAuthenticated;

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginModel);
                
                if (response.IsSuccessStatusCode)
                {
                    _isAuthenticated = true;
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Giriş yapılırken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public void Logout()
        {
            _isAuthenticated = false;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Register", registerModel);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kayıt olurken hata oluştu: {ex.Message}");
                return false;
            }
        }
    }
} 