using BusinessObjects_PhongChongMaTuy.DTOS;
using BusinessObjects_PhongChongMaTuy.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend_PhongChongMaTuy.Services
{
    public class AuthService
    {
        private readonly DrugPreventionDBContext _context;
        private readonly JwtService _jwtService;

        public AuthService(DrugPreventionDBContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existing != null) throw new Exception("Email already registered");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = request.Password, // không mã hóa
                Role = "Member"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse { Token = token, FullName = user.FullName, Role = user.Role.ToString() };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || user.PasswordHash != request.Password)
                throw new Exception("Invalid credentials");

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse { Token = token, FullName = user.FullName, Role = user.Role.ToString() };
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
