using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects_PhongChongMaTuy.DTOS
{
    // ===== REGISTER / LOGIN MODELS =====
    public class RegisterRequest
    {
        public string FullName { get; set; } // Gộp từ FirstName + LastName ở phía frontend hoặc controller
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
