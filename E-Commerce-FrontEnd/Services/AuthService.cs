using E_Commerce_FrontEnd.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System;
using System.Text.Json;

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

        private async Task SaveAuthDataToStorage()
        {
            try
            {
                var authJson = JsonSerializer.Serialize(_authData);
                // LocalStorage yerine SessionStorage kullanıyoruz
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", AUTH_DATA_KEY, authJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auth verisi kaydedilirken hata: {ex.Message}");
            }
        }

        public async Task LoadAuthDataFromStorage()
        {
            try
            {
                //  SessionStoragedaki auth verileri okunuyor
                var authJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", AUTH_DATA_KEY);
                if (!string.IsNullOrEmpty(authJson))
                {
                    _authData = JsonSerializer.Deserialize<AuthResponse>(authJson);
                    _isAuthenticated = true;
                    OnAuthenticationChanged?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auth verisi yüklenirken hata: {ex.Message}");
            }
        }

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
                        await SaveAuthDataToStorage();
                        OnAuthenticationChanged?.Invoke();
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

        public async Task Logout()
        {
            try
            {
                _authData = null;
                _isAuthenticated = false;
                // SessionStorage'dan auth verisini siliyoruz
                await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", AUTH_DATA_KEY);
                OnAuthenticationChanged?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Çıkış yapılırken hata oluştu: {ex.Message}");
            }
        }

        public async Task InitializeAuthenticationState()
        {
            try
            {
                // localStorage yerine sessionStorage kullanıyoruz
                var storedAuthData = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", AUTH_DATA_KEY);
                if (!string.IsNullOrEmpty(storedAuthData))
                {
                    _authData = JsonSerializer.Deserialize<AuthResponse>(storedAuthData);
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