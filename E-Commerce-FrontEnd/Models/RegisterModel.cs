namespace E_Commerce_FrontEnd.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; } = new List<string> { "user" };
    }
} 