using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using OnlineJobPortal.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        //  Login Endpoint 
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || loginDto.Email == null || loginDto.Password == null)
            {
                return BadRequest(new { Message = "Invalid login request" });
            }
            var user = _authService.AuthenticateUser(loginDto.Email, loginDto.Password); 
            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            var token = GenerateJwtToken(user);
            if (token == null)
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            return Ok(new
            {
                Token = token,
                User = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.Role
                }
            });
        }

        // Signup  
        [HttpPost("Signup")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null || registerDto.Email == null || registerDto.Password == null || registerDto.Role == null)
            {
                return BadRequest(new { Message = "Invalid registration request" });
            }
            // Check if user already exists
            var existingUser = _authService.GetUserByEmail(registerDto.Email);
            if (existingUser != null)
                return BadRequest(new { Message = "Email already registered" });

            var user = new ApplicationUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password, 
                Role = registerDto.Role,
                isActive = true,
                CreatedAt = DateTime.Now
            };

            _authService.RegisterUser(user);

            return Ok(new { Message = "User registered successfully" });
        }

        //  JWT Generation 
        private string? GenerateJwtToken(ApplicationUser user)
        {
            if (user.Email == null || user.Role == null) return null;

            var jwtSettings = _configuration.GetSection("Jwt");
            var keyString = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                return null;
            }

            var key = Encoding.UTF8.GetBytes(keyString);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("role", user.Role),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                                Issuer = issuer,
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
