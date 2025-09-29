using OnlineJobPortal.DTOs;

namespace OnlineJobPortal.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}