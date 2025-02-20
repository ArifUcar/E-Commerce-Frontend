using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface IAuthService
    {
        event Action OnAuthenticationChanged;
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Register(RegisterModel registerModel);
        void Logout();
        bool IsAuthenticated { get; }
        UserInfo CurrentUser { get; }
        string Token { get; }
        Task InitializeAuthenticationState();
    }
} 