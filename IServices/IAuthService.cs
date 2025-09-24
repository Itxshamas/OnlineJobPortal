using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.IServices
{
    public interface IAuthService
    {
        LoginResponseDto? AuthenticateUser(string email, string password);
        bool RegisterUser(RegisterDto registerDto);
        ApplicationUser? GetUserByEmail(string email);
    }
}
