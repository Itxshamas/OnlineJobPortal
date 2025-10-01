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

        // LOGIN 
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest(new { Message = "Invalid login request" });

            LoginResponseDto response = _authService.AuthenticateUser(loginDto.Email, loginDto.Password);
            if (response == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            return Ok(response);
        }

        //  SIGNUP 
        [HttpPost("signup")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null ||
                string.IsNullOrEmpty(registerDto.Email) ||
                string.IsNullOrEmpty(registerDto.Password) ||
                string.IsNullOrEmpty(registerDto.Role))
            {
                return BadRequest(new { Message = "Invalid registration request" });
            }

            bool result = _authService.RegisterUser(registerDto);
            if (!result)
                return BadRequest(new { Message = "Email already registered" });

            return Ok(new { Message = "User registered successfully" });
        }
    }
}
