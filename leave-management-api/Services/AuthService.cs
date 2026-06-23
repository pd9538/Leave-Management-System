using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using leave_management_api.Data;
using leave_management_api.DTOs;
using leave_management_api.Interfaces;
using leave_management_api.Models;

namespace leave_management_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly LeaveDbContext _context;

        private readonly IJwtService _jwtService;

        public AuthService(
            LeaveDbContext context,
            IJwtService jwtService)
        {
            _context = context;

            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                throw new Exception("Email already exists");

            var employeeRole = await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleName == "Employee");

            if (employeeRole == null)
            {
                employeeRole = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = "Employee"
                };

                _context.Roles.Add(employeeRole);

                await _context.SaveChangesAsync();
            }

            var user = new User
            {
                Id = Guid.NewGuid(),

                FullName = dto.FullName,

                Email = dto.Email,

                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                RoleId = employeeRole.Id
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            var leaveBalance = new LeaveBalance
            {
                Id = Guid.NewGuid(),

                UserId = user.Id,

                SickLeave = 10,

                CasualLeave = 12,

                EarnedLeave = 15
            };

            _context.LeaveBalances.Add(leaveBalance);

            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return null;

            bool verified = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!verified)
                return null;

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Role!.RoleName);

            return new AuthResponseDto
            {
                Token = token,

                Email = user.Email,

                Role = user.Role.RoleName
            };
        }
    }
}