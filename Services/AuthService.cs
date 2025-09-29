using OnlineJobPortal.Data;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineJobPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //  LOGIN 
        public LoginResponseDto? AuthenticateUser(string email, string password)
        {
            ApplicationUser user = _context.ApplicationUsers
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user == null)
                return null;

            string token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                User = MapToUserDto(user)
            };
        }

        //  REGISTER 
        public bool RegisterUser(RegisterDto registerDto)
        {
            if (_context.ApplicationUsers.Any(u => u.Email == registerDto.Email))
                return false; 

            ApplicationUser user = new ApplicationUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password, 
                Role = registerDto.Role,
                isActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
            return true;
        }

        //  GET USER BY EMAIL 
        public ApplicationUser? GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        private UserDto MapToUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.isActive,
                CreatedAt = user.CreatedAt
            };
        }

        //  HELPER: GENERATE JWT 
        private string GenerateJwtToken(ApplicationUser user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role)
            };

            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
