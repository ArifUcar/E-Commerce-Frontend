using E_Commerce_FrontEnd.Models;

namespace E_Commerce_FrontEnd.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginModel loginModel);
        void Logout();
        bool IsAuthenticated { get; }
    }
} 