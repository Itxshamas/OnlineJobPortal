using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;

namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //  Login Endpoint 
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || loginDto.Email == null || loginDto.Password == null)
            {
                return BadRequest(new { Message = "Invalid login request" });
            }
            LoginResponseDto loginResponse = _authService.AuthenticateUser(loginDto.Email, loginDto.Password); 
            if (loginResponse == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            return Ok(loginResponse);
        }

        // Signup  
        [HttpPost("Signup")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null || registerDto.Email == null || registerDto.Password == null || registerDto.Role == null)
            {
                return BadRequest(new { Message = "Invalid registration request" });
            }

            if (!_authService.RegisterUser(registerDto))
            {
                return BadRequest(new { Message = "Email already registered" });
            }

            return Ok(new { Message = "User registered successfully" });
        }
    }
}
