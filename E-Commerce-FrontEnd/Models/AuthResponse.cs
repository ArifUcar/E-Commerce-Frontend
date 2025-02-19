namespace E_Commerce_FrontEnd.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
} 