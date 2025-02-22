using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System;

namespace E_Commerce_FrontEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private bool _isAuthenticated;
        private AuthResponse _authData;
        private const string AUTH_DATA_KEY = "auth_data";

        public event Action OnAuthenticationChanged;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _isAuthenticated = false;
        }

        public bool IsAuthenticated => _isAuthenticated;
        public UserInfo CurrentUser => _authData?.User;
        public string Token => _authData?.Token;

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginModel);
                
                if (response.IsSuccessStatusCode)
                {
                    _authData = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    if (_authData != null)
                    {
                        _isAuthenticated = true;
                        await SaveAuthDataToLocalStorage();
                        OnAuthenticationChanged?.Invoke();

                        // Admin rolü kontrolü ve yönlendirme için true değerini döndür
                        return true;
                    }
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
            _authData = null;
            OnAuthenticationChanged?.Invoke();
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AUTH_DATA_KEY);
        }

        private async Task SaveAuthDataToLocalStorage()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AUTH_DATA_KEY, 
                System.Text.Json.JsonSerializer.Serialize(_authData));
        }

        public async Task InitializeAuthenticationState()
        {
            try
            {
                var storedAuthData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AUTH_DATA_KEY);
                if (!string.IsNullOrEmpty(storedAuthData))
                {
                    _authData = System.Text.Json.JsonSerializer.Deserialize<AuthResponse>(storedAuthData);
                    if (_authData != null)
                    {
                        _isAuthenticated = true;
                        // Token'ı HttpClient'a ekle
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authData.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kimlik durumu başlatılırken hata oluştu: {ex.Message}");
            }
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

        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/ChangePassword", model);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şifre değiştirme işlemi sırasında hata: {ex.Message}");
                return false;
            }
        }
    }
} 