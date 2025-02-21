using E_Commerce_FrontEnd.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace E_Commerce_FrontEnd.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public UserService(HttpClient httpClient, IJSRuntime jsRuntime)
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

        public async Task<List<User>> GetUsers()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<List<User>>("api/User");
                return response ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcılar alınırken hata oluştu: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<User> GetUser(Guid id)
        {
            try
            {
                await SetAuthHeader();
                return await _httpClient.GetFromJsonAsync<User>($"api/User/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı alınırken hata oluştu: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.PostAsJsonAsync("api/User", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı oluşturulurken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.PutAsJsonAsync($"api/User/{user.Id}", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı güncellenirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.DeleteAsync($"api/User/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı silinirken hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<int> GetTotalUserCount()
        {
            try
            {
                await SetAuthHeader();
                var response = await _httpClient.GetFromJsonAsync<UserCountResponse>("api/User/count");
                return response?.TotalUsers ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı sayısı alınırken hata oluştu: {ex.Message}");
                return 0;
            }
        }
    }
} 