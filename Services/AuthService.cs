
using OnlineJobPortal.IServices;
using OnlineJobPortal.Data;
using OnlineJobPortal.Models;
using System.Linq;
using OnlineJobPortal.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace OnlineJobPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public LoginResponseDto? AuthenticateUser(string email, string password)
        {
            ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            if (user == null)
            {
                return null;
            }

            string token = GenerateJwtToken(user);
            if (token == null)
            {
                return null;
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.isActive,
                CreatedAt = user.CreatedAt
            };

            return new LoginResponseDto
            {
                Token = token,
                User = userDto
            };
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        public bool RegisterUser(RegisterDto registerDto)
        {
            if (_context.ApplicationUsers.Any(u => u.Email == registerDto.Email))
            {
                return false; // User already exists
            }

            ApplicationUser user = new ApplicationUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password, // Note: Hashing should be implemented here
                Role = registerDto.Role,
                isActive = true,
                CreatedAt = DateTime.Now
            };

            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
            return true;
        }

        private string? GenerateJwtToken(ApplicationUser user)
        {
            if (user.Email == null || user.Role == null) return null;

            IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
            string keyString = jwtSettings["Key"];
            string issuer = jwtSettings["Issuer"];
            string audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                return null;
            }

            byte[] key = Encoding.UTF8.GetBytes(keyString);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("role", user.Role),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
