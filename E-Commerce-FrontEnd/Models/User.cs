using System;
using System.Collections.Generic;

namespace E_Commerce_FrontEnd.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; } // Sadece yeni kullanıcı oluşturma ve güncelleme için
    }
} 